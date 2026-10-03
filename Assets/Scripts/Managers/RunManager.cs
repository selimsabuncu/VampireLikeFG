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
    }
}
