using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TweenComponents
{
    public class ImageFillAmountTween : TweenBase
    {
        public float StartValue;

        [Space]
        public bool SetCurrentFillAsFinishValue;
        public float FinishValue;

        [Space]
        public Image TargetImage;


        private float _valueToExecute;

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
            if (SetCurrentFillAsFinishValue)
            {
                FinishValue = TargetImage.fillAmount;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartValue = TargetImage.fillAmount;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TargetImage;
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            if (straight)
            {
                _valueToExecute = FinishValue;
            }
            else
            {
                _valueToExecute = StartValue;
            }

            _tween = TargetImage.DOFillAmount(_valueToExecute, Duration)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }

        public override void ResetValue(bool applyStartValue = true)
        {
            if (applyStartValue)
            {
                TargetImage.fillAmount = StartValue;
            }
            else
            {
                TargetImage.fillAmount = FinishValue;
            }
        }
    }
}