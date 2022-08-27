using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TweenComponents;
using UnityEngine;
using UnityEngine.Rendering;

namespace TweenComponents
{
    public class VolumeWeightTween : TweenBase
    {
        public float StartValue;

        [Space]
        public bool SetCurrentWeightAsFinishValue;
        public float FinishValue;

        [Space]
        public Volume TargetVolume;


        private float _valueToExecute;

        protected override void Awake()
        {
            if (!TargetVolume)
            {
                TargetVolume = GetComponent<Volume>();
            }

            if (TargetVolume)
            {
                TargetVolume.useGUILayout = false;

                CheckValueOverrideOnAwake();
            }
            else
            {
                Debug.LogError($"There`s no Volume for tween on {gameObject.name}!");
            }
        }

        protected override void CheckValueOverrideOnAwake()
        {
            if (SetCurrentWeightAsFinishValue)
            {
                FinishValue = TargetVolume.weight;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartValue = TargetVolume.weight;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TargetVolume;
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

            _tween = DOTween.To(() => TargetVolume.weight, value => TargetVolume.weight = value, _valueToExecute, Duration)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }

        public override void ResetValue(bool applyStartValue = true)
        {
            if (applyStartValue)
            {
                TargetVolume.weight = StartValue;
            }
            else
            {
                TargetVolume.weight = FinishValue;
            }
        }
    }
}

