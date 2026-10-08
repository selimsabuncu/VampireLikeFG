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
        public float cooldown = 5;
        public int spawnRange = 20;
        public string[] enemiesToSpawn;
        //TODO: Spawn enemies randomly by weighted enemy chance
        
        private IEnumerator Start()
        {
            _spawnCoroutine = StartCoroutine(EnemiesSpawning());
            
            while (true)
            {
                switch (RunManager.Instance.CurrentTime)
                {
                    case >=120f:
                        cooldown = 2f;
                        break;
                    case >=60f:
                        cooldown = 3.5f;
                        break;
                }

                yield return new WaitForSeconds(5f);
            }
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
