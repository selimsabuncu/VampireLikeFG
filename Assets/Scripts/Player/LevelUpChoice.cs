using Managers;
using Managers.GameStates;
using Player.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class LevelUpChoice : MonoBehaviour
    {
        private Weapon _newWeapon;
        private WeaponInstance _weaponToUpgrade;

        [SerializeField] private Image weaponIcon;
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private TMP_Text weaponDescription;

        public void SetNewWeapon(Weapon weapon)
        {
            _newWeapon = weapon;
            _weaponToUpgrade = null;

            weaponIcon.sprite = weapon.attackSprite;
            weaponName.text = weapon.weaponName;
            weaponDescription.text = weapon.weaponDescription;
        }

        public void SetWeaponUpgrade(WeaponInstance weapon)
        {
            _newWeapon = null;
            _weaponToUpgrade = weapon;
            weaponIcon.sprite = weapon.Weapon.attackSprite;
            weaponName.text = weapon.Weapon.weaponName;
            WeaponLevelData nextLevel = weapon.Weapon.levels[weapon.Level];
            weaponDescription.text = $"{nextLevel.StatToLevel} +{nextLevel.Value}";
        }

        public void LevelUpWeapon()
        {
            if (_newWeapon != null)
            {
                WeaponManager.Instance.AddWeapon(_newWeapon);
                Debug.Log($"Added {_newWeapon.weaponName}");
            }
            else if (_weaponToUpgrade != null)
            {
                _weaponToUpgrade.LevelUp();
                Debug.Log($"Upgraded {_weaponToUpgrade.Weapon.weaponName} " +
                          $"to level {_weaponToUpgrade.Level}");
            }

            PlayerLevelManager.Instance.ActivateLevelUpScreen(false);
            GameManager.Instance.SwitchState<PlayState>();
        }
    }
}