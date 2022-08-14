using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TweenComponents
{
    public class TextMeshAlphaTween : TweenBase
    {
        public float StartAlpha;

        [Space]
        public bool SetCurrentAlphaAsFinishValue;
        public float EndAlpha;

        [Space]
        public TMP_Text TargetText;

        private Color _targetColor;

        protected override void Awake()
        {
            if (!TargetText)
            {
                TargetText = GetComponent<TextMeshPro>();
            }

            if (TargetText)
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
                EndAlpha = TargetText.color.a;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartAlpha = TargetText.color.a;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TargetText;
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            _targetColor = TargetText.color;

            if (straight)
            {
                _targetColor.a = EndAlpha;
            }
            else
            {
                _targetColor.a = StartAlpha;
            }

            _tween = TargetText.DOColor(_targetColor, Duration)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }

        public override void ResetValue(bool applyStartValue = true)
        {
            var color = TargetText.color;

            if (applyStartValue)
            {
                color.a = StartAlpha;
            }
            else
            {
                color.a = EndAlpha;
            }

            TargetText.color = color;
        }
    }
}