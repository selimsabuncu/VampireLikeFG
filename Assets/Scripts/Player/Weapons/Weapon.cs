using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    //TODO: could be simplified and optimized better. Too complex and mind confusing at the moment.
    //Targeting system uses FindObjects for enemies each time a weapon is going to shoot. Could be just getting from objectPooling
    [CreateAssetMenu(menuName = "Weapons/Weapon")]
    public class Weapon : ScriptableObject
    {
        public Sprite attackSprite;
        public GameObject attackPrefab;
        
        public WeaponBaseData baseData;
        public List<WeaponLevelData> levels = new List<WeaponLevelData>(8);

        public WeaponBehaviourType behaviour;
        public WeaponTargetingType targeting;
    }
}