using DG.Tweening;
using System;
using UnityEngine;

namespace TweenComponents
{
    public class TweenBase : MonoBehaviour
    {
        [Header("Base Settings")]
        public TweenBaseSettings BaseSettings;

        [Space]
        [Header("Loop Settings")]
        public TweenLoopSettings LoopSettings;


        [Space]
        [Header("Execution Settings")]
        public TweenExecutionSettings ExecutionSettings;

        [Space]
        [Header("Value Settings")]
        public bool OverrideValueOnAwake;

        #region Properties

        public float Delay
        {
            get => BaseSettings.StartDelay;
            set => BaseSettings.StartDelay = value;
        }

        public float Duration
        {
            get => BaseSettings.Duration;
            set => BaseSettings.Duration = value;
        }

        public Ease EaseType
        {
            get => BaseSettings.EaseType;
            set => BaseSettings.EaseType = value;
        }


        public int LoopCount
        {
            get => LoopSettings.LoopCount;
            set => LoopSettings.LoopCount = value;
        }

        public LoopType LoopType
        {
            get => LoopSettings.LoopType;
            set => LoopSettings.LoopType = value;
        }


        public bool CanBeInterrupted
        {
            get => ExecutionSettings.CanBeInterrupted;
            set => ExecutionSettings.CanBeInterrupted = value;
        }

        public bool ExecuteOnEnable
        {
            get => ExecutionSettings.ExecuteOnEnable;
            set => ExecutionSettings.ExecuteOnEnable = value;
        }

        public bool StraightOnEnable
        {
            get => ExecutionSettings.StraightOnEnable;
            set => ExecutionSettings.StraightOnEnable = value;
        }

        public bool ResetValueOnExecute
        {
            get => ExecutionSettings.ResetValueOnExecute;
            set => ExecutionSettings.ResetValueOnExecute = value;
        }


        public bool IsCompleted { get; protected set; }

        #endregion

        protected Tween _tween;
        protected bool _straight;

        public bool IsStraight => _straight;

        public event Action OnStarted;
        public event Action OnCompleted;

        protected virtual void Awake()
        {
            CheckValueOverrideOnAwake();
        }

        protected virtual void CheckValueOverrideOnAwake()
        {
            if (OverrideValueOnAwake)
            {
                ResetValue();
            }
            else
            {
                ApplyCurrentValueAsStartValue();
            }
        }

        protected virtual void ApplyCurrentValueAsStartValue() { }

        protected virtual void OnEnable()
        {
            if (ExecuteOnEnable)
            {
                Execute(StraightOnEnable);
            }
        }

        public virtual bool CanBeExecuted()
        {
            if (_tween != null && _tween.IsPlaying())
            {
                return CanBeInterrupted;
            }

            return true;
        }

        public virtual void Execute(bool straight = true)
        {
            if (ResetValueOnExecute)
            {
                Stop();
                ResetValue(straight);
            }

            _straight = straight;
            IsCompleted = false;
            OnStarted?.Invoke();
        }

        [ContextMenu("Stop")]
        public virtual void Stop()
        {
            if (_tween != null)
            {
                _tween.Rewind();
            }
        }

        public virtual void Kill()
        {
            if (_tween != null)
            {
                _tween.Kill();
            }
        }

        public virtual void Pause()
        {
            if (_tween != null)
            {
                _tween.Pause();
            }
        }

        public virtual void OnTweenCompleted()
        {
            _tween = null;
            IsCompleted = true;
            OnCompleted?.Invoke();
        }

        public virtual void ResetValue(bool applyStartValue = true) { }


#if UNITY_EDITOR

        [ContextMenu("Execute")]
        private void MenuExecuteStraight()
        {
            Execute();
        }

        [ContextMenu("Execute Reversed")]
        private void MenuExecuteReversed()
        {
            Execute(false);
        }

#endif
    }
}
