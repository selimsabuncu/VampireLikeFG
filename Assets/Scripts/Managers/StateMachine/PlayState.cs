using Player;
using UnityEngine;

namespace Managers
{
    public class PlayState : State
    {
        public override void UpdateState()
        {
            base.UpdateState();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.SwitchState<PauseState>();
            }
        }
    }
}