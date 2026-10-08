using System.Collections;
using DG.Tweening;
using Managers.GameStates;
using UnityEngine;

namespace Extensions
{
    public class GameMusicLoop : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject audioObject;
        [SerializeField] private float loopStart;
        [SerializeField] private float loopEnd;
    
        private IEnumerator Start()
        {
            while (audioSource.isPlaying)
            {
                if (audioSource.time >= loopEnd)
                {
                    audioSource.time = loopStart;
                }

                yield return new WaitForSeconds(0.2f);

                if (GameManager.Instance.IsState<GameOverState>())
                {
                    audioSource.DOFade(0, 1f).OnComplete(() =>
                    {
                        audioSource.Stop();
                        Destroy(this.gameObject);
                    });
                }
            }
        }
    }
}
