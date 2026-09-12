using UnityEngine;

namespace Player.Weapons
{
    public static class TargetingSystem
    {
        public static Transform GetTarget(WeaponTargetingType targetingType, WeaponInstance weapon)
        {
            switch (targetingType)
            {
                case WeaponTargetingType.NearestEnemy:
                    return GetNearestEnemy(weapon);

                case WeaponTargetingType.FarthestEnemy:
                    return GetFarthestEnemy(weapon);

                case WeaponTargetingType.RandomEnemy:
                    return GetRandomEnemy(weapon);

                case WeaponTargetingType.LowestHealthEnemy:
                    return GetLowestHealthEnemy(weapon);

                case WeaponTargetingType.HighestHealthEnemy:
                    return GetHighestHealthEnemy(weapon);

                default:
                    return null;
            }
        }

        private static Transform GetNearestEnemy(WeaponInstance weapon)
        {
            Enemy.Enemy[] enemies = Object.FindObjectsByType<Enemy.Enemy>(FindObjectsSortMode.None);
            if (enemies.Length == 0) return null;
            Transform nearestEnemy = null;
            float nearestDistanceSqr = float.MaxValue;

            Vector3 weaponPosition = PlayerController.Instance.transform.position;

            foreach (Enemy.Enemy enemy in enemies)
            {
                float distanceSqr = (enemy.transform.position - weaponPosition).sqrMagnitude;

                if (distanceSqr < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distanceSqr;
                    nearestEnemy = enemy.transform;
                }
            }

            return nearestEnemy;
        }


        private static Transform GetFarthestEnemy(WeaponInstance weapon)
        {
            // Find farthest enemy
            return null;
        }

        private static Transform GetRandomEnemy(WeaponInstance weapon)
        {
            // Find random enemy
            return null;
        }

        private static Transform GetLowestHealthEnemy(WeaponInstance weapon)
        {
            // Find lowest HP enemy
            return null;
        }

        private static Transform GetHighestHealthEnemy(WeaponInstance weapon)
        {
            // Find highest HP enemy
            return null;
        }
    }
}