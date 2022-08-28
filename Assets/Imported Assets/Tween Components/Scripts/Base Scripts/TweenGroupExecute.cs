using System;
using System.Linq;
using TweenComponents;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TweenGroupExecute : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool _getTweensOnAwake;
    [SerializeField] private bool _findMaxDurationTweenOnExecute;
    [Header("Tweens")]
    [SerializeField] private List<TweenBase> _tweensToExecute;

    private TweenBase _maxDurationTween;

    public event Action OnCompleted;

    private void Awake()
    {
        if (_tweensToExecute == null || _tweensToExecute.Count == 0 || _getTweensOnAwake)
        {
            GetTweensFromChilds();
        }

        GetMaxDurationTween();
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
        if (_findMaxDurationTweenOnExecute)
        {
            GetMaxDurationTween();
        }

        _maxDurationTween.OnCompleted += OnTweenCompleted;

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

    [ContextMenu("Get Tweens From Childs")]
    private void GetTweensFromChilds()
    {
        if (_tweensToExecute == null)
        {
            _tweensToExecute = new List<TweenBase>();
        }

        var tweens = GetComponentsInChildren<TweenBase>().Where(tween => !_tweensToExecute.Contains(tween));

        //remove null or missing elements
        _tweensToExecute = _tweensToExecute.Where(tween => tween).ToList();

        _tweensToExecute.AddRange(tweens);

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(this);
        }
#endif
    }

    private void GetMaxDurationTween()
    {
        _maxDurationTween = _tweensToExecute.OrderByDescending(tween => tween.Duration).FirstOrDefault();

        if (_maxDurationTween)
        {
            _maxDurationTween.OnCompleted += OnTweenCompleted;
        }
    }

    private void OnTweenCompleted()
    {
        _maxDurationTween.OnCompleted -= OnTweenCompleted;

        OnCompleted?.Invoke();
    }

#if UNITY_EDITOR

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
