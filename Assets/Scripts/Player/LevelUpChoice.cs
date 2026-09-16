using System;
using Player.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class LevelUpChoice : MonoBehaviour
    {
        [SerializeField] private Image weaponIcon;
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private TMP_Text weaponDescription;
        public Weapon weapon;
        
        private void OnEnable()
        {
            
        }
    }
}
