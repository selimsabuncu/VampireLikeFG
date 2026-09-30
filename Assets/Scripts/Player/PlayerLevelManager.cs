using System.Collections.Generic;
using Extensions;
using Managers.GameStates;
using Player.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerLevelManager : MonoSingleton<PlayerLevelManager>
    {
        [SerializeField] private GameObject levelUpScreen;

        private Weapon[] _weapons;
        private LevelUpChoice[] _levelUpChoices;

        private void Start()
        {
            _weapons = Resources.LoadAll<Weapon>("WeaponData&Prefabs");
            _levelUpChoices = levelUpScreen.GetComponentsInChildren<LevelUpChoice>(true);
            levelUpScreen.SetActive(false);
        }

        public void ActivateLevelUpScreen(bool setTo)
        {
            if (setTo)
            {
                RandomizeOptions();
                GameManager.Instance.SwitchState<UpgradeState>();
            }

            levelUpScreen.SetActive(setTo);
        }

        private void RandomizeOptions()
        {
            foreach (LevelUpChoice choice in _levelUpChoices)
            {
                bool chooseNewWeapon = Random.value > 0.5f;

                if (chooseNewWeapon)
                {
                    Weapon weapon = GetRandomNewWeapon();

                    if (weapon != null)
                    {
                        choice.SetNewWeapon(weapon);
                        continue;
                    }
                }
                
                WeaponInstance weaponToUpgrade = GetRandomUpgradeableWeapon();

                if (weaponToUpgrade != null)
                {
                    choice.SetWeaponUpgrade(weaponToUpgrade);
                }
            }
        }

        private Weapon GetRandomNewWeapon()
        {
            List<Weapon> availableWeapons = new();

            IReadOnlyList<WeaponInstance> ownedWeapons = WeaponManager.Instance.GetWeapons();

            foreach (Weapon weapon in _weapons)
            {
                bool alreadyOwned = false;

                foreach (WeaponInstance ownedWeapon in ownedWeapons)
                {
                    if (ownedWeapon.Weapon == weapon)
                    {
                        alreadyOwned = true;
                        break;
                    }
                }

                if (!alreadyOwned)
                {
                    availableWeapons.Add(weapon);
                }
            }

            if (availableWeapons.Count == 0) return null;

            return availableWeapons[Random.Range(0, availableWeapons.Count)];
        }

        private WeaponInstance GetRandomUpgradeableWeapon()
        {
            IReadOnlyList<WeaponInstance> ownedWeapons = WeaponManager.Instance.GetWeapons();
            List<WeaponInstance> upgradeableWeapons = new();
            foreach (WeaponInstance weapon in ownedWeapons)
            {
                if (weapon.Level <= weapon.Weapon.levels.Count)
                {
                    upgradeableWeapons.Add(weapon);
                }
            }
            if (upgradeableWeapons.Count == 0) return null;
            return upgradeableWeapons[Random.Range(0, upgradeableWeapons.Count)];
        }
    }
}
