using System;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class SettingsManager : MonoSingleton<SettingsManager>
    {
        [SerializeField] private GameObject[] panels;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        
        private void Start()
        {
            OpenSettings(false);
            
            resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        }
        
        public void OpenSettings(bool state)
        {
            this.gameObject.SetActive(state);
        }

        public void ChangePanel(GameObject panelToOpen)
        {
            foreach (var panel in panels)
            {
                panel.SetActive(false);
            }
            panelToOpen.SetActive(true);
        }

        #region Graphics

        private void ChangeResolution(int index)
        {
            string option = resolutionDropdown.options[index].text;

            switch (option)
            {
                case "1920x1080":
                    Screen.SetResolution(1920, 1080, true);
                    break;

                case "640x480":
                    Screen.SetResolution(640, 480, true);
                    break;
                
                default:
                    Debug.LogWarning("Screen resolution not found");
                    break;
            }
        }
        
        

        #endregion
    }
}
