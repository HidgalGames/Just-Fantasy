using DG.Tweening;
using UnityEngine;

namespace TweenComponents
{
    public class CanvasGroupFadeTween : TweenBase
    {
        public float StartAlpha;

        [Space]
        public bool SetCurrentAlphaAsFinishValue;
        public float EndAlpha;

        [Space]
        public CanvasGroup TargetCanvasGroup;

        private float _targetAlpha;

        protected override void Awake()
        {
            if (!TargetCanvasGroup)
            {
                TargetCanvasGroup = GetComponent<CanvasGroup>();
            }

            if (TargetCanvasGroup)
            {
                CheckValueOverrideOnAwake();
            }
            else
            {
                Debug.LogError($"There`s no Image for tween on {gameObject.name}!");
            }
        }

        protected override void CheckValueOverrideOnAwake()
        {
            if (SetCurrentAlphaAsFinishValue)
            {
                EndAlpha = TargetCanvasGroup.alpha;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartAlpha = TargetCanvasGroup.alpha;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TargetCanvasGroup;
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            _targetAlpha = TargetCanvasGroup.alpha;

            if (straight)
            {
                _targetAlpha = EndAlpha;
            }
            else
            {
                _targetAlpha = StartAlpha;
            }

            _tween = TargetCanvasGroup.DOFade(_targetAlpha, Duration)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }

        public override void ResetValue(bool applyStartValue = true)
        {
            if (applyStartValue)
            {
                TargetCanvasGroup.alpha = StartAlpha;
            }
            else
            {
                TargetCanvasGroup.alpha = EndAlpha;
            }
        }
    }

}