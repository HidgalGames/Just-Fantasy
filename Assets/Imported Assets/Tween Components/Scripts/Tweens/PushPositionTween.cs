using DG.Tweening;
using UnityEngine;

namespace TweenComponents
{
    public class PushPositionTween : TweenBase
    {
        public Vector3 PushDelta;

        [Space]
        [Min(0)] public int BounceCount;
        [Range(0f, 1f)] public float Elasticity;

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

            _tween = TransformToChange.DOPunchPosition(PushDelta, Duration, BounceCount, Elasticity)
                .SetDelay(Delay)
                .SetEase(EaseType)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(() => OnTweenCompleted());
        }
    }
}
