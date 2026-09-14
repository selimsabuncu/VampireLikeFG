using UnityEngine;

namespace Player.Weapons
{
    public class WeaponProjectile : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float projectileSpeed = 20f;
        public float projectileDamage = 0f;
        
        private void Update()
        {
            Vector3 direction = target.position - transform.position;
            
            var vector3 = transform.position;
            vector3.z = direction.z;
            transform.position = vector3;
            
            transform.Translate(direction.normalized * (projectileSpeed * Time.deltaTime), Space.World);
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= .5f)
            {
                target.GetComponent<Enemy.Enemy>().TakeDamage(projectileDamage);
                Destroy(gameObject); //TODO: Carry to ObjectPooling as well. 15647
            }
        }
    }
}
