using System;
using Extensions;
using Player.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerLevelManager : MonoSingleton<PlayerLevelManager>
    {
        /*onLevelUp
         bring out the level UI
         buttons will have levelUp methods from this script
         levelUp method will activate WeaponManager levelUp
         get rid of levelUpUI
         */
        
        private Weapons.Weapon[] _weapons;
        private LevelUpChoice[] _weaponChoice;
        
        private void Start()
        {
            Debug.Log("Start");
            _weapons =  Resources.LoadAll<Weapons.Weapon>("WeaponData&Prefabs");
            _weaponChoice = levelUpScreen.GetComponentsInChildren<LevelUpChoice>(true);
        }

        [SerializeField] private GameObject levelUpScreen;


        public void ActivateLevelUpScreen(bool setTo)
        {
            foreach (var choice in _weaponChoice)
            {
                var randomWeapon = _weapons[Random.Range(0, _weapons.Length)];
                choice.weapon = randomWeapon;
            }
            
            levelUpScreen.SetActive(setTo);
        }

        private void RandomizeOptions()
        {
            
        }
    }
}
