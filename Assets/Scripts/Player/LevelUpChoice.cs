using System;
using Managers;
using Managers.GameStates;
using Player.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player
{
    public class LevelUpChoice : MonoBehaviour
    {
        public Weapon weapon;
        [SerializeField] private Image weaponIcon;
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private TMP_Text weaponDescription;
        
        private void OnEnable()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            weaponIcon.sprite = weapon.attackSprite;
            weaponName.text = weapon.weaponName;
            weaponDescription.text = weapon.weaponDescription;
        }
        
        public void LevelUpWeapon()
        {
            Debug.Log($"LeveledUp {weapon.weaponName}");
            PlayerLevelManager.Instance.ActivateLevelUpScreen(false);
            GameManager.Instance.SwitchState<PlayState>();
        }
    }
}
