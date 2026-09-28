using System;
using Extensions;
using Managers;
using Managers.GameStates;
using Managers.ObservedUpdate;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoSingleton<PlayerController>, IUpdateObserver
    {
        private Vector3 _movement;
        [Header("BasicStats")]
        [SerializeField] private float movementSpeed = 5;
        [SerializeField] private float health = 100;
        [SerializeField] private Slider healthBar;
        [Header("Leveling")]
        [SerializeField] private float playerXP;
        [SerializeField] private int currentXP;
        [SerializeField] private int level;
        
        private void OnEnable()
        {
            UpdateManager.RegisterObserver(this);
        }

        private void OnDisable()
        {
            UpdateManager.UnregisterObserver(this);
        }

        public void ObservedUpdate()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            _movement.Normalize();
            _movement.z = 0;
        }
        
        private void FixedUpdate()
        {
            if (!GameManager.Instance.IsState<PlayState>()) return;
            
            transform.position += _movement * (movementSpeed * Time.fixedDeltaTime);
        }

        public void CollectedXP(int amountOfXP)
        {
            currentXP += amountOfXP;
            if (currentXP >= 100)
            {
                PlayerLevelManager.Instance.ActivateLevelUpScreen(true);
            }
        }
        
        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            healthBar.value = health/100;
            if (health <= 0) Die();
        }

        private void Die()
        {
            Debug.Log("you ded");
            //game over screen
        }
    }
}
