using System;
using Extensions;
using UnityEngine;

namespace Managers
{
    public class GameManager : StateMachine
    {
        public static GameManager Instance;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            SwitchState<PlayState>();
        }

        private void Update()
        {
            UpdateStateMachine();
        }
    }
}
