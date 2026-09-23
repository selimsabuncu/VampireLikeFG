using UnityEngine;

namespace Managers
{
    public class PauseState : State
    {
        public override void UpdateState()
        {
            base.UpdateState();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.SwitchState<PlayState>();
            }
        }
    }
}