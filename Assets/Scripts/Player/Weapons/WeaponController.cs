using System;
using System.Collections.Generic;
using Managers;
using Managers.GameStates;
using Managers.ObservedUpdate;
using UnityEngine;

namespace Player.Weapons
{
    public class WeaponController : MonoBehaviour, IUpdateObserver
    {
        private WeaponManager weaponManager;
        private ProjectileBehaviour projectileBehaviour;
        private readonly Dictionary<WeaponInstance, float> cooldownTimers = new();

        private void Awake()
        {
            weaponManager = GetComponent<WeaponManager>();
            projectileBehaviour = GetComponent<ProjectileBehaviour>();
        }

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
            
            foreach (WeaponInstance weapon in weaponManager.weapons)
            {
                if (!cooldownTimers.ContainsKey(weapon))
                {
                    cooldownTimers.Add(weapon, 0f);
                }

                cooldownTimers[weapon] -= Time.deltaTime;

                if (cooldownTimers[weapon] > 0f) continue;
                ActivateWeapon(weapon);

                cooldownTimers[weapon] = weapon.Cooldown;
                
                Debug.Log($"Level: {weapon.Level} Damage: {weapon.Damage} Cooldown: {weapon.Cooldown}");
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