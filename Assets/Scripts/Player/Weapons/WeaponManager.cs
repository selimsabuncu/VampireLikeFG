using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Player.Weapons
{
    public class WeaponManager : MonoSingleton<WeaponManager>
    {
        [SerializeField] private List<Weapon> startingWeapons;

        [SerializeField] public List<WeaponInstance> weapons = new();

        private void Awake() => InitializeWeapons(); 

        private void InitializeWeapons()
        {
            foreach (Weapon weapon in startingWeapons)
            {
                AddWeapon(weapon);
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            if (weapon == null) return;

            WeaponInstance instance = new WeaponInstance(weapon);
            weapons.Add(instance);
        }

        public IReadOnlyList<WeaponInstance> GetWeapons()
        {
            return weapons;
        }
    }
}