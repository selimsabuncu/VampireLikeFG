using System;
using Extensions;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        private Vector3 _movement;
        [SerializeField] private float movementSpeed = 5;
        [SerializeField] private float health = 100;
        [SerializeField] private Slider healthBar;
        
        private void Update()
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

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            healthBar.value = health/100;
            if (health <= 0) Die();
        }

        public void Die()
        {
            Debug.Log("you ded");
            //game over screen
        }
    }
}
