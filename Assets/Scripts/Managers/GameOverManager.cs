using System.Collections;
using DG.Tweening;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameOverManager : MonoSingleton<GameOverManager>
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Transform endPoint;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TMP_Text highScoreText;
        
        private void Start()
        {
            SetChildrenActive(false);
        }

        [ContextMenu("TestGameOverMovement")]
        public void GameOverActivate()
        {
            SetChildrenActive(true);
            highScoreText.text = RunManager.Instance.GetCurrentTimeString();
            gameOverPanel.GetComponent<Image>().DOFade(1f, 3f);
            StartCoroutine(GameOverCoroutine());
        }

        private IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            transform.DOMoveY(endPoint.position.y, 2.5f);
            //dust effect
            //sound effect
        }

        private void SetChildrenActive(bool active)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
            }
        }

        public void ReturnToMenu()
        {
            //fadeIn
            SceneManager.LoadScene("MainMenu");
        }

        public void RestartGame()
        {
            //fadeIn
            SceneManager.LoadScene("GameScene");
        }
    }
}
