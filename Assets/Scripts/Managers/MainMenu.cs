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
