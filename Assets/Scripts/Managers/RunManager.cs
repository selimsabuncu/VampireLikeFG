using System;
using Extensions;
using Managers.ObservedUpdate;
using UnityEngine;

namespace Managers
{
    public class RunManager : MonoSingleton<RunManager>, IUpdateObserver
    {
        public float CurrentTime { get; private set; }

        private void OnEnable()
        {
            UpdateManager.RegisterObserver(this);
        }

        private void OnDisable()
        {
            UpdateManager.UnregisterObserver(this);
        }

        public void ObservedUpdate()
        {
            CurrentTime += Time.deltaTime;
        }

        public string GetHighScoreString()
        {
            return TimeSpan.FromSeconds(PlayerPrefs.GetFloat("timeScore")).ToString(@"hh\:mm\:ss");
        }
        
        public string GetCurrentTimeString()
        {
            return TimeSpan.FromSeconds(CurrentTime).ToString(@"hh\:mm\:ss");
        }
    }
}
