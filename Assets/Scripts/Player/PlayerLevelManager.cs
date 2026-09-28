using System;
using Extensions;
using Managers.GameStates;
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
        private LevelUpChoice[] _levelUpChoice;
        [SerializeField] private GameObject levelUpScreen;

        private void Start()
        {
            _weapons =  Resources.LoadAll<Weapons.Weapon>("WeaponData&Prefabs");
            _levelUpChoice = levelUpScreen.GetComponentsInChildren<LevelUpChoice>(true);
            levelUpScreen.SetActive(false);
        }

        public void ActivateLevelUpScreen(bool setTo)
        {
            RandomizeOptions();

            if (setTo == true) GameManager.Instance.SwitchState<UpgradeState>();
            levelUpScreen.SetActive(setTo);
        }
        
        private void RandomizeOptions()
        {
            foreach (var choice in _levelUpChoice)
            {
                choice.weapon = _weapons[Random.Range(0, _weapons.Length)];
                choice.UpdateUI();
            }
        }
    }
}
