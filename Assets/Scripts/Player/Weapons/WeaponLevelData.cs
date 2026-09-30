using UnityEngine;

namespace Player.Weapons
{
    [System.Serializable]
    public class WeaponLevelData
    {
        [SerializeField] private WeaponLevelType statToLevel;
        [SerializeField] private float value;

        public WeaponLevelType StatToLevel => statToLevel;
        public float Value => value;

        public void ApplyTo(WeaponInstance weapon)
        {
            switch (statToLevel)
            {
                case WeaponLevelType.Damage:
                    weapon.Damage += value;
                    break;

                case WeaponLevelType.Cooldown:
                    weapon.Cooldown -= value;
                    break;
            }
        }
    }
}