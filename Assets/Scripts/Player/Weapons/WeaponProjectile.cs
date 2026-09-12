using System;
using UnityEngine;

namespace Player.Weapons
{
    public class WeaponProjectile : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float projectileSpeed = 20f;

        private void Update()
        {
            Vector3 direction = target.position - transform.position;
            
            var vector3 = transform.position;
            vector3.z = direction.z;
            transform.position = vector3;
            
            transform.Translate(direction.normalized * (projectileSpeed * Time.deltaTime), Space.World);
        }
    }
}
