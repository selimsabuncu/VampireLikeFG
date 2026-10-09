using System.Collections;
using Managers;
using Managers.GameStates;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawn : MonoBehaviour
    {
        private Coroutine _spawnCoroutine;

        [Header("Spawn Settings")]
        public float cooldown = 5f;
        public int spawnRange = 20;

        [Header("Enemy Settings")]
        public string[] enemiesToSpawn = { "BasicEnemy", "ExplodingEnemy" };

        [SerializeField] private float explodingEnemyStartTime = 15f;

        [Range(0f, 1f)] [SerializeField] private float explodingEnemyChance = 0.15f;

        private IEnumerator Start()
        {
            _spawnCoroutine = StartCoroutine(EnemiesSpawning());

            while (true)
            {
                if (RunManager.Instance != null)
                {
                    switch (RunManager.Instance.CurrentTime)
                    {
                        case >= 60f:
                            cooldown = 2f;
                            break;
                        case >= 30f:
                            cooldown = 3.5f;
                            break;
                        default:
                            cooldown = 5f;
                            break;
                    }
                }

                yield return new WaitForSeconds(5f);
            }
        }

        private IEnumerator EnemiesSpawning()
        {
            while (true)
            {
                if (GameManager.Instance != null
                    && GameManager.Instance.IsState<PlayState>()
                    && PlayerController.Instance != null
                    && RunManager.Instance != null)
                {
                    SpawnEnemy();
                }

                yield return new WaitForSeconds(cooldown);
            }
        }

        private void SpawnEnemy()
        {
            Vector3 playerPosition = PlayerController.Instance.transform.position;
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnLocation = new Vector2(playerPosition.x, playerPosition.y) 
                                    + direction 
                                    * Random.Range(spawnRange * 0.5f, spawnRange);

            string enemyToSpawn = "BasicEnemy";
            
            if (RunManager.Instance.CurrentTime >= explodingEnemyStartTime 
                && Random.value < explodingEnemyChance)
            {
                enemyToSpawn = "ExplodingEnemy";
            }

            ObjectPooling.ObjectPooling.Instance.SpawnFromPool(
                enemyToSpawn,
                spawnLocation,
                Quaternion.identity
            );
        }
    }
}