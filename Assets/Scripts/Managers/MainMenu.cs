using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text highScoreText;

        private void OnEnable()
        {
            highScoreText.text = "High score:\n" + RunManager.Instance.GetHighScoreString();
        }

        public void PlayGame()
        {
            //load? change scenes
            SceneManager.LoadScene("GameScene");
        }

        public void SettingsMenu(bool setting)
        {
            SettingsManager.Instance.OpenSettings(setting);
        }
        
        public void QuitGame()
        {
            //save
            Application.Quit();
        }
    }
}
