using DG.Tweening;
using UnityEngine;

namespace TweenComponents
{
    public class PushScaleTween : TweenBase
    {
        public Vector3 ScaleChange;
        public int BounceCount;
        [Range(0, 1)] public float Elasticity;

        [Space]
        public Transform TransformToChange;

        protected override void Awake()
        {
            if (!TransformToChange)
            {
                TransformToChange = transform;
            }

            base.Awake();
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            _tween = TransformToChange
                .DOPunchScale(ScaleChange, Duration, BounceCount, Elasticity)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }
    }
}