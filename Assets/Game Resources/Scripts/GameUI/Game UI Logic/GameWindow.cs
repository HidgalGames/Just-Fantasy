using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameUI.Logic
{
    public class GameWindow : MonoBehaviour
    {
        [field: SerializeField] public string WindowName { get; private set; }
        [SerializeField] private TweenGroupExecute _animationController;
        
        [Header("Settings")]
        [SerializeField] private bool _canBeDisabled = true;

        [field: Space]
        [field: SerializeField] public bool IsAdditive { get; private set; } = true;
        [field: SerializeField] public bool IsBlockingPlayerInput { get; private set; }
        [field: SerializeField] public bool IsCursorVisible { get; private set; }

        //for debug
        [field: Header("Debug")]
        [field: ReadOnly] [field: SerializeField] public bool IsOpened { get; private set; } = true;

        private List<Canvas> _canvases;

        public event Action<GameWindow, bool> OnWindowStateChanged;

        private void Awake()
        {
            _canvases = new List<Canvas>(GetComponentsInChildren<Canvas>());
            _canvases.Add(GetComponent<Canvas>());
        }

        public void SwitchState()
        {
            if (IsOpened && !_canBeDisabled) return;

            SetActive(!IsOpened);
        }

        public void SetActive(bool isActive)
        {
            if (!isActive && !_canBeDisabled) return;

            _animationController.OnCompleted -= DelayedDisabling;

            IsOpened = isActive;

            _animationController.Execute(isActive);

            if(!IsOpened && _animationController)
            {
                //if we has animation controller then play window close animation
                _animationController.OnCompleted += DelayedDisabling;
            }
            else
            {
                foreach (var canvas in _canvases)
                {
                    canvas.enabled = isActive;
                }
            }

            OnWindowStateChanged?.Invoke(this, IsOpened);
        }

        private void DelayedDisabling()
        {
            _animationController.OnCompleted -= DelayedDisabling;

            foreach (var canvas in _canvases)
            {
                canvas.enabled = false;
            }
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (gameObject.name == WindowName) return;

            WindowName = gameObject.name;
            EditorUtility.SetDirty(this);
        }

#endif
    }
}

