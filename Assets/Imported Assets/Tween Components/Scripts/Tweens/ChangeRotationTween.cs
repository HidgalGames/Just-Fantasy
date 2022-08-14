using DG.Tweening;
using UnityEngine;

namespace TweenComponents
{
    public class ChangeRotationTween : TweenBase
    {
        public Vector3 StartValue;

        [Space]
        public bool SetCurrentRotationAsFinishValue;
        public Vector3 FinishValue;

        [Space]
        public Transform TransformToChange;

        private Vector3 _target;

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
            if (SetCurrentRotationAsFinishValue)
            {
                FinishValue = TransformToChange.localRotation.eulerAngles;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartValue = TransformToChange.localRotation.eulerAngles;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TransformToChange;
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            _target = straight ? FinishValue : StartValue;

            _tween = TransformToChange.DOLocalRotate(_target, Duration)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }

        public override void ResetValue(bool applyStartValue = true)
        {
            if (applyStartValue)
            {
                TransformToChange.localRotation = Quaternion.Euler(StartValue);
            }
            else
            {
                TransformToChange.localRotation = Quaternion.Euler(FinishValue);
            }
        }
    }
}