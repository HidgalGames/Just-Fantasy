using UnityEngine;

namespace GameUI.Logic
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Blockable _blockable;
        [SerializeField] private bool _cursorStateOnStart = false;
        [Header("Debug")]
        [SerializeField] private KeyCode _hotkeySwitcher = KeyCode.H;

        public bool IsBlocked => _blockable.IsBlocked;

        private void Awake()
        {
            Cursor.visible = _cursorStateOnStart;
        }

        public void HideCursor()
        {
            _blockable.SetBlocked(false);

            if (!IsBlocked)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

        public void ShowCursor()
        {
            _blockable.SetBlocked(true);

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
