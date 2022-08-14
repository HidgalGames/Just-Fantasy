using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TweenComponents
{
    public class ImageAlphaTween : TweenBase
    {
        public float StartAlpha;

        [Space]
        public bool SetCurrentAlphaAsFinishValue;
        public float EndAlpha;

        [Space]
        public Image TargetImage;

        private Color _targetColor;

        protected override void Awake()
        {
            if (!TargetImage)
            {
                TargetImage = GetComponent<Image>();
            }

            if (TargetImage)
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
                EndAlpha = TargetImage.color.a;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartAlpha = TargetImage.color.a;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TargetImage;
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            _targetColor = TargetImage.color;

            if (straight)
            {
                _targetColor.a = EndAlpha;
            }
            else
            {
                _targetColor.a = StartAlpha;
            }

            _tween = TargetImage.DOColor(_targetColor, Duration)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }

        public override void ResetValue(bool applyStartValue = true)
        {
            var color = TargetImage.color;

            if (applyStartValue)
            {
                color.a = StartAlpha;
            }
            else
            {
                color.a = EndAlpha;
            }

            TargetImage.color = color;
        }
    }
}