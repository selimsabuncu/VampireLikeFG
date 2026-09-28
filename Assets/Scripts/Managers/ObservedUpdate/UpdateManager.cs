using System.Collections.Generic;
using Managers.GameStates;
using UnityEngine;

namespace Managers.ObservedUpdate
{
    public class UpdateManager : MonoBehaviour
    {
        private static List<IUpdateObserver> _observers = new List<IUpdateObserver>();
        private static List<IUpdateObserver> _pendingObservers = new List<IUpdateObserver>();
        private static int _currentIndex;
        
        //TODO: Static members persist through scenes, so if you load another scene, you need to ensure
        //your UpdateManager has no leftover observers from the previous scene, otherwise it would just fail to
        //access an unloaded object. Cheap and dirty method would be to clean the collection on Awake or Start.
        
        private void Update()
        {
            if (!GameManager.Instance.IsState<PlayState>()) return;
            
            for (_currentIndex = _observers.Count - 1; _currentIndex >= 0; _currentIndex--)
            {
                _observers[_currentIndex].ObservedUpdate();
            }
            
            _observers.AddRange(_pendingObservers);
            _pendingObservers.Clear();
        }

        public static void RegisterObserver(IUpdateObserver observer)
        {
            _observers.Add(observer);
        }

        public static void UnregisterObserver(IUpdateObserver observer)
        {
            _observers.Remove(observer);
            _currentIndex--;
        }
    }
}
