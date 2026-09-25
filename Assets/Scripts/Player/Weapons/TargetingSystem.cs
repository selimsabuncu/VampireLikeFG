using UnityEngine;

namespace Player.Weapons
{
    public static class TargetingSystem
    {
        public static Transform GetTarget(WeaponTargetingType targetingType)
        {
            switch (targetingType)
            {
                case WeaponTargetingType.NearestEnemy:
                    return GetNearestEnemy();
                case WeaponTargetingType.FarthestEnemy:
                    return GetFarthestEnemy();
                case WeaponTargetingType.RandomEnemy:
                    return GetRandomEnemy();
                case WeaponTargetingType.LowestHealthEnemy:
                    return GetLowestHealthEnemy();
                case WeaponTargetingType.HighestHealthEnemy:
                    return GetHighestHealthEnemy();
                default:
                    return null;
            }
        }

        private static Transform GetNearestEnemy()
        {
            Enemy.Enemy[] enemies = GameObject.FindObjectsByType<Enemy.Enemy>();
            if (enemies.Length == 0) return null;
            Transform nearestEnemy = null;
            float nearestDistanceSqr = float.MaxValue;

            Vector3 currentPosition = PlayerController.Instance.transform.position;

            foreach (Enemy.Enemy enemy in enemies)
            {
                float distanceSqr = (enemy.transform.position - currentPosition).sqrMagnitude;

                if (distanceSqr < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distanceSqr;
                    nearestEnemy = enemy.transform;
                }
            }

            return nearestEnemy;
        }


        private static Transform GetFarthestEnemy()
        {
            Enemy.Enemy[] enemies = GameObject.FindObjectsByType<Enemy.Enemy>();
            if (enemies.Length == 0) return null;
            Transform farthestEnemy = null;
            float farthestDistanceSqr = 0;
            
            Vector3 currentPosition = PlayerController.Instance.transform.position;
            
            foreach (Enemy.Enemy enemy in enemies)
            {
                float distanceSqr = (enemy.transform.position - currentPosition).sqrMagnitude;

                if (distanceSqr > farthestDistanceSqr)
                {
                    farthestDistanceSqr = distanceSqr;
                    farthestEnemy = enemy.transform;
                }
            }

            return farthestEnemy;
        }

        private static Transform GetRandomEnemy()
        {
            Enemy.Enemy[] enemies = GameObject.FindObjectsByType<Enemy.Enemy>();
            if (enemies.Length == 0) return null;
            Transform randomEnemy = null;

            randomEnemy = enemies[Random.Range(0, enemies.Length)].transform;

            return randomEnemy;
        }

        private static Transform GetLowestHealthEnemy()
        {
            Enemy.Enemy[] enemies = GameObject.FindObjectsByType<Enemy.Enemy>();
            if (enemies.Length == 0) return null;
            Transform weakestEnemy = enemies[0].transform;
            float weakestHealth = weakestEnemy.GetComponent<Enemy.Enemy>().Health;
            
            foreach (Enemy.Enemy enemy in enemies)
            {
                float checkHealth = enemy.Health;
                
                if (checkHealth < weakestHealth)
                {
                    weakestHealth = checkHealth;
                    weakestEnemy = enemy.transform;
                }
            }

            return weakestEnemy;
        }

        private static Transform GetHighestHealthEnemy()
        {
            Enemy.Enemy[] enemies = GameObject.FindObjectsByType<Enemy.Enemy>();
            if (enemies.Length == 0) return null;
            Transform strongestEnemy = enemies[0].transform;
            float strongestHealth = strongestEnemy.GetComponent<Enemy.Enemy>().Health;
            
            Vector3 currentPosition = PlayerController.Instance.transform.position;
            
            foreach (Enemy.Enemy enemy in enemies)
            {
                float checkHealth = enemy.Health;
                
                if (checkHealth > strongestHealth)
                {
                    strongestHealth = checkHealth;
                    strongestEnemy = enemy.transform;
                }
            }

            return strongestEnemy;
        }
    }
}