using System.Collections.Generic;
using Managers.GameStates;
using UnityEngine;

namespace Managers.ObservedUpdate
{
    public class UpdateManager : MonoBehaviour
    {
        private static readonly List<IUpdateObserver> _observers = new();
        private static readonly List<IUpdateObserver> _pendingObservers = new();
        private static readonly HashSet<IUpdateObserver> _pendingRemovals = new();

        private void Update()
        {
            if (GameManager.Instance == null ||
                !GameManager.Instance.IsState<PlayState>())
                return;

            // Apply removals before iterating.
            if (_pendingRemovals.Count > 0)
            {
                _observers.RemoveAll(
                    observer => _pendingRemovals.Contains(observer));

                _pendingRemovals.Clear();
            }

            // Apply registrations before iterating.
            if (_pendingObservers.Count > 0)
            {
                foreach (var observer in _pendingObservers)
                {
                    if (!_observers.Contains(observer))
                        _observers.Add(observer);
                }

                _pendingObservers.Clear();
            }

            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                IUpdateObserver observer = _observers[i];

                // Defensive cleanup for destroyed Unity objects.
                if (observer is MonoBehaviour behaviour &&
                    !behaviour.isActiveAndEnabled)
                {
                    _observers.RemoveAt(i);
                    continue;
                }

                observer.ObservedUpdate();
            }
        }

        public static void RegisterObserver(IUpdateObserver observer)
        {
            if (observer == null)
                return;

            _pendingRemovals.Remove(observer);

            if (!_observers.Contains(observer) &&
                !_pendingObservers.Contains(observer))
            {
                _pendingObservers.Add(observer);
            }
        }

        public static void UnregisterObserver(IUpdateObserver observer)
        {
            if (ReferenceEquals(observer, null))
                return;

            _pendingObservers.Remove(observer);
            _pendingRemovals.Add(observer);
        }

        private void OnDestroy()
        {
            _observers.Clear();
            _pendingObservers.Clear();
            _pendingRemovals.Clear();
        }
    }
}