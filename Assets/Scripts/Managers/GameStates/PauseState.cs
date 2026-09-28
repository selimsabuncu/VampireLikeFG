using UnityEngine;

namespace Managers.GameStates
{
    public class PauseState : State
    {
        public override void UpdateState()
        {
            base.UpdateState();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.SwitchState<PlayState>();
                SettingsManager.Instance.OpenSettings(false);
            }
        }
    }
}