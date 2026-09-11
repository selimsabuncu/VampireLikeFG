using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Weapons
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float level;


        private void Attack() { }
        private void DealDamage() => PlayerController.Instance.TakeDamage(damage);
    }
}
