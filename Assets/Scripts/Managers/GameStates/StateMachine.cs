using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class StateMachine : MonoBehaviour
    {
        public List<State> States = new List<State>();
        public State CurrentState;
        
        public void SwitchState<aState>()
        {
            foreach (State state in States)
            {
                if (state.GetType() == typeof(aState))
                {
                    CurrentState?.ExitState();
                    CurrentState = state;
                    CurrentState.EnterState();
                    Signals.Instance.OnChangeGameState.Invoke(CurrentState);
                    Debug.Log($"Switching state to {state.GetType().Name}");
                    return;
                }
            }
            
            Debug.LogWarning("State does not exist.");
        }

        public virtual void UpdateStateMachine()
        {
            CurrentState?.UpdateState();
        }

        public bool IsState<aState>()
        {
            if (!CurrentState) return false;
            return CurrentState.GetType() == typeof(aState);
        }
    }
}