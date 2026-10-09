using System.Collections;
using Extensions;
using UnityEngine;
using Managers;
using Managers.GameStates;
using Player;

namespace Enemy
{
    public class ExplodingEnemy : Enemy
    {
        [Header("Explosion Settings")]
        [SerializeField] private float explosionRadius = 3f;
        [SerializeField] private float explosionDamage = 25f;
        [SerializeField] private float fuseTime = 2.5f;

        [Header("Flash Settings")]
        [SerializeField] private Color flashColor = Color.red;
        [SerializeField] private float flashInterval = 0.4f;
        [SerializeField] private float minimumFlashInterval = 0.08f;

        private bool _isExploding;
        private Coroutine _explosionCoroutine;
        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;

        protected override string PoolKey => "ExplodingEnemy";

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_spriteRenderer != null)
            {
                _originalColor = _spriteRenderer.color;
            }
        }

        protected override void Move()
        {
            if (_isExploding || PlayerController.Instance == null) return;

            base.Move();

            float distance = Vector3.Distance(transform.position, 
                PlayerController.Instance.transform.position);

            if (distance <= explosionRadius / 2)
            {
                _explosionCoroutine = StartCoroutine(Explode());
            }
        }

        private IEnumerator Explode()
        {
            _isExploding = true;

            float elapsed = 0f;
            bool isFlashing = false;

            while (elapsed < fuseTime)
            {
                if (_spriteRenderer != null)
                {
                    isFlashing = !isFlashing;
                    _spriteRenderer.color = isFlashing ? flashColor : _originalColor;
                }
                
                float progress = elapsed / fuseTime;
                float interval = Mathf.Lerp(flashInterval, minimumFlashInterval, progress);

                yield return new WaitForSeconds(interval);
                elapsed += interval;
            }

            if (!isActiveAndEnabled) yield break;

            if (GameManager.Instance != null
                && GameManager.Instance.IsState<PlayState>()
                && PlayerController.Instance != null)
            {
                float distance = Vector3.Distance(
                    transform.position,
                    PlayerController.Instance.transform.position);

                if (distance <= explosionRadius)
                {
                    PlayerController.Instance.TakeDamage(explosionDamage);
                }
            }
            
            ResetFlash();

            //TODO: Spawn explosion particles and play explosion audio.

            _explosionCoroutine = null;
            Die();
        }

        protected override IEnumerator Attack()
        {
            while (true) yield return null;
        }

        protected override void Die()
        {
            if (_explosionCoroutine != null)
            {
                StopCoroutine(_explosionCoroutine);
                _explosionCoroutine = null;
            }
            
            ParticleHelper.Instance.SpawnParticleAtLocation("explodingParticle", transform.position);
            
            _isExploding = false;
            ResetFlash();

            base.Die();
        }

        private void ResetFlash()
        {
            if (_spriteRenderer != null) _spriteRenderer.color = _originalColor;
        }

        private void OnDisable()
        {
            _isExploding = false;
            _explosionCoroutine = null;
            ResetFlash();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}