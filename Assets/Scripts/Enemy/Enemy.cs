using System.Collections;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed = 5f;
        [SerializeField] protected float health = 100f;
        [SerializeField] protected float attackDamage = 5f;
        [SerializeField] protected float xpToDrop = 5f;
        [SerializeField] protected float attackRange = 1f;
        private Coroutine _attackCoroutine;
        
        //I don't really need to change these in-script so can get rid of them
        protected virtual float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        protected virtual float Health { get => health; set => health = value; }
        protected virtual float XpToDrop { get => health; set => health = value; }
        protected virtual float AttackDamage { get => health; set => health = value; }
        
        protected virtual void Start()
        {
            Debug.Log(gameObject.name);
            _attackCoroutine = StartCoroutine(Attack());
        }

        protected virtual void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            Vector3 direction = PlayerController.Instance.transform.position - transform.position;
            transform.Translate(direction.normalized * (MovementSpeed * Time.deltaTime), Space.World);
        }

        protected virtual IEnumerator Attack()
        {
            while (true)
            {
                float distance = Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
                if (distance <= attackRange)
                {
                    PlayerController.Instance.TakeDamage(attackDamage);
                }
                yield return new WaitForSeconds(1f);
            }
        }
        
        protected virtual void Die()
        {
            StopCoroutine(_attackCoroutine);
            //die animation, sound effect, returnToPool, dropXP
        }
        
        
        #if UNITY_EDITOR
        [SerializeField] private bool gizmosOn;
        private void OnDrawGizmos()
        {
            if (!gizmosOn) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        #endif
    }
}
