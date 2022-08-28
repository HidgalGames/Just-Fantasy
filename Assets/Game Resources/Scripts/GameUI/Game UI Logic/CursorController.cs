using UnityEngine;

namespace GameUI.Logic
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private bool _cursorStateOnStart = false;
        [Header("Debug")]
        [SerializeField] private KeyCode _hotkeySwitcher = KeyCode.H;
        [ReadOnly] [SerializeField] private int _blockersCount = 0;

        private void Awake()
        {
            Cursor.visible = _cursorStateOnStart;
        }

        public void HideCursor()
        {
            if (_blockersCount > 0)
            {
                _blockersCount--;
            }

            if (_blockersCount <= 0)
            {
                _blockersCount = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

        public void ShowCursor()
        {
            _blockersCount++;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void SwitchState()
        {
            SetState(!Cursor.visible);
        }

        public void SetState(bool isVisible)
        {
            if (isVisible)
            {
                ShowCursor();
            }
            else
            {
                HideCursor();
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(_hotkeySwitcher))
            {
                SwitchState();
            }
        }
#endif
    }

}
