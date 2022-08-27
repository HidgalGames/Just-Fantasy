using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenComponents
{
    public class TweenSequence : MonoBehaviour
    {
        [Header("Execution Settings")]
        public bool ExecuteOnEnable;
        public bool StraightOnEnable = true;
        public bool ThrowStraightStateInTweens = false;
        [Space]
        public bool CanBeInterrupted = true;
        [Space]
        public List<TweenBase> Sequence;


        private int _currentIndex;
        private bool _isStraight;

        public TweenBase CurrentTween { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsPlaying { get; private set; }

        public event Action OnCompleted;

        private void OnEnable()
        {
            if (ExecuteOnEnable)
            {
                Execute(StraightOnEnable);
            }
        }

        public void Execute(bool straight = true)
        {
            if (IsPlaying && !CanBeInterrupted) return;
            if (Sequence.Count < 1) return;

            Stop();

            _isStraight = straight;
            _currentIndex = _isStraight ? 0 : Sequence.Count - 1;

            IsPlaying = true;
            IsCompleted = false;

            StartTween();
        }

        private void OnTweenCompleted()
        {
            CurrentTween.OnCompleted -= OnTweenCompleted;

            _currentIndex += _isStraight ? 1 : -1;

            if(_currentIndex < 0 || _currentIndex >= Sequence.Count)
            {
                Stop();

                IsCompleted = true;
                OnCompleted?.Invoke();
                return;
            }

            StartTween();
        }

        private void StartTween()
        {
            CurrentTween = Sequence[_currentIndex];
            CurrentTween.OnCompleted += OnTweenCompleted;

            if (ThrowStraightStateInTweens)
            {
                CurrentTween.Execute();
            }
            else
            {
                CurrentTween.Execute();
            }
        }

        [ContextMenu("Stop")]
        public void Stop()
        {
            if (CurrentTween)
            {
                CurrentTween.OnCompleted -= OnTweenCompleted;
                CurrentTween = null;
            }

            IsPlaying = false;
        }

#if UNITY_EDITOR

        [ContextMenu("Execute")]
        public void ExecuteStraight()
        {
            Execute();
        }

        [ContextMenu("Execute Reversed")]
        public void ExecuteReversed()
        {
            Execute(false);
        }

#endif
    }


}