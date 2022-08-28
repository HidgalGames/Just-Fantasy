using System;
using System.Linq;
using TweenComponents;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TweenGroupExecute : MonoBehaviour
{
    [SerializeField] private TweenBase[] _tweensToExecute;

    private TweenBase _maxDurationTween;

    public event Action OnCompleted;

    private void Awake()
    {
        _tweensToExecute = GetComponentsInChildren<TweenBase>();

        _maxDurationTween = _tweensToExecute.OrderByDescending(tween => tween.Duration).FirstOrDefault();

        if (_maxDurationTween)
        {
            _maxDurationTween.OnCompleted += OnTweenCompleted;
        }
    }

    private void OnDestroy()
    {
        if (_maxDurationTween)
        {
            _maxDurationTween.OnCompleted -= OnTweenCompleted;
        }
    }

    public void Execute(bool isStraight = true)
    {
        foreach (var tween in _tweensToExecute)
        {
            tween.Execute(isStraight);
        }
    }

    public void Stop()
    {
        foreach (var tween in _tweensToExecute)
        {
            tween.Stop();
        }
    }

    private void OnTweenCompleted()
    {
        OnCompleted?.Invoke();
    }

#if UNITY_EDITOR

    [ContextMenu("Get Tweens From Childs")]
    private void GetTweensFromChilds()
    {
        _tweensToExecute = GetComponentsInChildren<TweenBase>();
        EditorUtility.SetDirty(this);
    }

    [ContextMenu("Execute")]
    private void ExecuteStraight()
    {
        Execute();
    }

    [ContextMenu("Execute Reversed")]
    private void ExecuteReversed()
    {
        Execute(false);
    }

    [ContextMenu("Stop")]
    private void StopFromMenu()
    {
        Stop();
    }

#endif
}
