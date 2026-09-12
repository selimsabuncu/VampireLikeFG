using UnityEngine;

namespace Player.Weapons
{
    public class ProjectileBehaviour : WeaponBehaviour
    {
        public override void Activate(WeaponInstance weapon)
        {
            Debug.Log("Activating projectile");
            Transform target = TargetingSystem.GetTarget(weapon.Weapon.targeting, weapon);
            if (target == null) return;
            
            GameObject newGO = Instantiate(weapon.Weapon.attackPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
            
            WeaponProjectile projectile = newGO.GetComponent<WeaponProjectile>();
            
            if (projectile == null)
            {
                Debug.LogError($"Attack prefab '{weapon.Weapon.attackPrefab.name}' " + $"does not contain a WeaponProjectile component.");
                Destroy(newGO);
                return;
            }
            projectile.target = target;
        }
    }
}