using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public class ParticleHelper : MonoSingleton<ParticleHelper>
    {
        [SerializeField] private List<ParticleEntry> particleEntries;

        private Dictionary<string, GameObject> _particles;

        private void Awake()
        {
            base.Awake();
            _particles = new Dictionary<string, GameObject>();

            foreach (ParticleEntry entry in particleEntries)
            {
                if (entry.prefab == null)
                    continue;

                _particles[entry.id] = entry.prefab;
            }
        }

        public void SpawnParticleAtLocation(string particleId, Vector3 position)
        {
            if (!_particles.TryGetValue(particleId, out GameObject prefab))
            {
                Debug.LogWarning($"Particle '{particleId}' was not found.");
                return;
            }

            Instantiate(prefab, position, Quaternion.identity);
        }
    }
    
    [System.Serializable] 
    public class ParticleEntry
    {
        public string id;
        public GameObject prefab;
    }
}