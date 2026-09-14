using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        private WeaponManager weaponManager;
        private ProjectileBehaviour projectileBehaviour;
        private readonly Dictionary<WeaponInstance, float> cooldownTimers = new();

        private void Awake()
        {
            weaponManager = GetComponent<WeaponManager>();
            projectileBehaviour = GetComponent<ProjectileBehaviour>();
        }


        private void Update()
        {
            foreach (WeaponInstance weapon in weaponManager.weapons)
            {
                if (!cooldownTimers.ContainsKey(weapon))
                {
                    cooldownTimers.Add(weapon, 0f);
                }

                cooldownTimers[weapon] -= Time.deltaTime;

                if (cooldownTimers[weapon] > 0f) continue;
                ActivateWeapon(weapon);

                cooldownTimers[weapon] = weapon.Weapon.baseData.cooldown;
            }
        }

        private void ActivateWeapon(WeaponInstance weapon)
        {
            WeaponBehaviour behaviour = GetBehaviour(weapon.Weapon.behaviour);

            if (behaviour == null)
            {
                Debug.LogWarning($"No behaviour found for weapon: {weapon.Weapon.name}");
                return;
            }
            
            behaviour.Activate(weapon);
        }
        
        private WeaponBehaviour GetBehaviour(WeaponBehaviourType behaviourType)
        {
            switch (behaviourType)
            {
                case WeaponBehaviourType.Projectile: 
                    return projectileBehaviour;
                default: 
                    return null;
            }
        }
    }
}