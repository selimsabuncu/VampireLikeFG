using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPooling : MonoSingleton<ObjectPooling>
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> poolDictionary;

        void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform);  // Instantiate as a child of ObjectPooler
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return null;
            }
        
            // update so it creates a new object if there are no more in the pool
            if (poolDictionary[tag].Count == 0)
            {
                GameObject obj = Instantiate(pools.Find(x => x.tag == tag).prefab, transform);
                obj.SetActive(false);
                poolDictionary[tag].Enqueue(obj);
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            return objectToSpawn;
        }

        public void ReturnToPool(string tag, GameObject objectToReturn)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return;
            }

            objectToReturn.SetActive(false);
            objectToReturn.transform.SetParent(transform);  // Ensure it's parented back to the ObjectPooler
            poolDictionary[tag].Enqueue(objectToReturn);
        }
    }
}