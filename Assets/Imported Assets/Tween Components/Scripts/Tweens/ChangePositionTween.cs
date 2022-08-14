using DG.Tweening;
using UnityEngine;

namespace TweenComponents
{
    public class ChangePositionTween : TweenBase
    {
        public Vector3 StartPosition;

        [Space]
        public bool SetCurrentPositionAsFinishValue;
        public Vector3 FinishPosition;

        [Space]
        public Transform TransformToChange;

        private Vector3 _positionToExecute;

        protected override void Awake()
        {
            if (!TransformToChange)
            {
                TransformToChange = transform;
            }

            base.Awake();
        }

        protected override void CheckValueOverrideOnAwake()
        {
            if (SetCurrentPositionAsFinishValue)
            {
                FinishPosition = TransformToChange.localPosition;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartPosition = TransformToChange.localPosition;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TransformToChange;
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            if (ResetValueOnExecute)
            {
                ResetValue(straight);
            }

            if (straight)
            {
                _positionToExecute = FinishPosition;
            }
            else
            {
                _positionToExecute = StartPosition;
            }

            _tween = TransformToChange.DOLocalMove(_positionToExecute, Duration)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());

        }

        public override void ResetValue(bool applyStartValue = true)
        {
            if (applyStartValue)
            {
                TransformToChange.localPosition = StartPosition;
            }
            else
            {
                TransformToChange.localPosition = FinishPosition;
            }
        }
    }
}