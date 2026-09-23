using System;
using System.Collections;
using Managers;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawn : MonoBehaviour
    {
        private Coroutine _spawnCoroutine;
        public int cooldown = 5;
        public int spawnRange = 20;
        public string[] enemiesToSpawn;
        //TODO: Spawn enemies randomly by weighted enemy chance
        
        private void Start()
        {
            _spawnCoroutine = StartCoroutine(EnemiesSpawning());
        }

        private IEnumerator EnemiesSpawning()
        {
            while (true)
            {
                if (GameManager.Instance.IsState<PlayState>())
                {
                    Vector3 playerPosition = PlayerController.Instance.transform.position;
                    Vector2 spawnLocation = new Vector2(
                        Random.Range(playerPosition.x - spawnRange, playerPosition.x - spawnRange), 
                        Random.Range(playerPosition.y - spawnRange, playerPosition.y - spawnRange));
                
                    ObjectPooling.ObjectPooling.Instance.SpawnFromPool("BasicEnemy", spawnLocation, Quaternion.identity);
                }
                
                yield return  new WaitForSeconds(cooldown);
            }
        }
    }
}
