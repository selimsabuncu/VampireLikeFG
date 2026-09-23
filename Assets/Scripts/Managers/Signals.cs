using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class Signals : MonoSingleton<Signals>
    {
        public UnityAction<State> OnChangeGameState = delegate{ };
    }
}
