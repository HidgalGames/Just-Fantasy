using TweenComponents;
using UnityEngine;

public class ChangeStateOnTweenComplete : MonoBehaviour
{
    [SerializeField] private TweenBase _targetTween;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private bool _isInversed;

    private void Awake()
    {
        _targetTween.OnCompleted += OnTweenCompleted;
    }

    private void OnDestroy()
    {
        _targetTween.OnCompleted -= OnTweenCompleted;
    }

    private void OnTweenCompleted()
    {
        _targetObject.SetActive(_isInversed ? !_targetTween.IsStraight : _targetTween.IsStraight);
    }
}
