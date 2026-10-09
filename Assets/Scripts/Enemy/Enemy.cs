using System;
using System.Collections;
using Extensions;
using Managers;
using Managers.GameStates;
using Managers.ObservedUpdate;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IUpdateObserver
    {
        [SerializeField] protected float movementSpeed = 5f;
        [SerializeField] protected float health = 100f;
        [SerializeField] protected float currentHealth = 100f;
        [SerializeField] protected float attackDamage = 5f;
        [SerializeField] protected float xpToDrop = 5f;
        [SerializeField] protected float attackRange = 1f;
        private Coroutine _attackCoroutine;
        
        //I don't really need to change these in-script so can get rid of them
        public virtual float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public virtual float CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public virtual float XpToDrop { get => xpToDrop; set => xpToDrop = value; }
        public virtual float AttackDamage { get => attackDamage; set => attackDamage = value; }
        protected virtual string PoolKey => "BasicEnemy";
        
        private void OnEnable()
        {
            _attackCoroutine = StartCoroutine(Attack());
            UpdateManager.RegisterObserver(this);
            
            if (RunManager.Instance.CurrentTime < 60f) { CurrentHealth = health; }
            else { CurrentHealth = health * RunManager.Instance.CurrentTime / 60f; }
        }

        private void OnDisable()
        {
            UpdateManager.UnregisterObserver(this);
        }

        public void ObservedUpdate()
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
            //TODO: Am I supposed add a new one each time? how to fix this shit we wonder
            yield return new WaitUntil(() => 
                GameManager.Instance != null
                && PlayerController.Instance != null
                && RunManager.Instance != null);
            
            while (true)
            {
                if (GameManager.Instance.IsState<PlayState>())
                {
                    float distance = Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
                    
                    if (distance <= attackRange)
                    {
                        PlayerController.Instance.TakeDamage(attackDamage);
                    }
                }
                
                yield return new WaitForSeconds(1f);
            }
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        
        protected virtual void Die()
        {
            AudioManager.Instance.PlayAudioAtLocation("deathScream", transform.position, true);
            GameObject xp = ObjectPooling.ObjectPooling.Instance.SpawnFromPool("xpDrop", transform.position, Quaternion.identity);
            xp.GetComponent<XPDrop>().xpValue = XpToDrop;
            StopCoroutine(_attackCoroutine);
            ObjectPooling.ObjectPooling.Instance.ReturnToPool(PoolKey, gameObject);
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
