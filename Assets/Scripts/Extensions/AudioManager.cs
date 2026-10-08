using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private List<AudioEntry> audioEntries;
        private Dictionary<string, List<AudioClip>> _audios;

        protected override void Awake()
        {
            base.Awake();
            
            _audios = new Dictionary<string, List<AudioClip>>();

            foreach (AudioEntry entry in audioEntries)
            {
                if (entry.AudioClip == null) continue;

                if (!_audios.ContainsKey(entry.id))
                {
                    _audios[entry.id] = new List<AudioClip>();
                }

                _audios[entry.id].Add(entry.AudioClip);
            }
        }

        public void PlayAudioAtLocation(string audioID, Vector3 position, bool spatialSound = false)
        {
            if (!_audios.TryGetValue(audioID, out List<AudioClip> clips) || clips.Count == 0)
            {
                Debug.LogWarning($"Audio '{audioID}' was not found.");
                return;
            }
            
            AudioClip audioClip = clips[Random.Range(0, clips.Count)];

            GameObject audioObject = new GameObject($"Audio_{audioID}");
            audioObject.transform.position = position;

            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;

            if (spatialSound)
            {
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.minDistance = 6f;
                audioSource.maxDistance = 25f;
            }

            audioSource.Play();

            Destroy(audioObject, audioClip.length);
        }
    }

    [System.Serializable]
    public class AudioEntry
    {
        public string id;
        public AudioClip AudioClip;
    }
}