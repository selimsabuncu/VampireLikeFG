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

            Debug.Log($"WeaponManager: {weaponManager}");
            Debug.Log($"ProjectileBehaviour: {projectileBehaviour}");
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

                if (cooldownTimers[weapon] > 0f)
                    continue;

                ActivateWeapon(weapon);

                cooldownTimers[weapon] =
                    weapon.Weapon.baseData.cooldown;
            }
        }

        private void ActivateWeapon(WeaponInstance weapon)
        {
            Debug.Log($"Weapon: {weapon.Weapon.name}");
            Debug.Log($"Behaviour Type: {weapon.Weapon.behaviour}");

            WeaponBehaviour behaviour = GetBehaviour(weapon.Weapon.behaviour);

            if (behaviour == null)
            {
                Debug.LogWarning(
                    $"No behaviour found for weapon: {weapon.Weapon.name}"
                );

                return;
            }

            Debug.Log($"Behaviour Component: {behaviour.GetType().Name}");

            behaviour.Activate(weapon);
        }


        private WeaponBehaviour GetBehaviour(WeaponBehaviourType behaviourType)
        {
            Debug.Log($"Getting behaviour for: {behaviourType}");
            Debug.Log($"Projectile Behaviour reference: {projectileBehaviour}");

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