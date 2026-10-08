using Extensions;
using Player;

namespace Managers.GameStates
{
    public class GameOverState : State
    {
        public override void EnterState()
        {
            base.EnterState();
            
            AudioManager.Instance.PlayAudioAtLocation("gameOver", PlayerController.Instance.transform.position);
        }
    }
}
