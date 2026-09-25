using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            //load? change scenes
            SceneManager.LoadScene("GameScene");
        }

        public void SettingsMenu()
        {
            SettingsManager.Instance.OpenSettings(true);
        }
        
        public void QuitGame()
        {
            //save
            Application.Quit();
        }
    }
}
