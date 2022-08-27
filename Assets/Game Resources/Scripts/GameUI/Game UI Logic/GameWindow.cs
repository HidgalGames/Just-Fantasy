using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameUI
{
    public class GameWindow : MonoBehaviour
    {
        [field: SerializeField] public string WindowName { get; private set; }
        
        [Header("Settings")]
        [SerializeField] private bool _canBeDisabled = true;

        //for debug
        [Header("Debug")]
        [ReadOnly] [SerializeField] private bool _isEnabled;

        private Canvas[] _canvases;

        private void Awake()
        {
            _canvases = GetComponentsInChildren<Canvas>();
            _isEnabled = _canvases[0].enabled;
        }

        public void SetActive(bool isActive)
        {
            if (!isActive && !_canBeDisabled) return;

            _isEnabled = isActive;

            foreach (var canvas in _canvases)
            {
                canvas.enabled = isActive;
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

