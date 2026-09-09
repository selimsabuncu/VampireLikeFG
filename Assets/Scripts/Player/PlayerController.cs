using System;
using Extensions;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        private Vector3 _movement;
        [SerializeField] private float movementSpeed;
        
        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            _movement.Normalize();
            _movement.z = 0;
        }

        private void FixedUpdate()
        {
            transform.position += _movement * (movementSpeed * Time.fixedDeltaTime);
        }
    }
}
