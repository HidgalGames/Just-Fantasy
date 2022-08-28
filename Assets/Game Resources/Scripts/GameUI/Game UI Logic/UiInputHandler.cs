using UnityEngine;
using Zenject;

namespace GameUI.Logic
{
    public class UiInputHandler : MonoBehaviour
    {
        private const string INVENTORY_BUTTON_NAME = "Inventory Button";

        [SerializeField] private CursorController _cursorController;
        [SerializeField] private GameWindowsHandler _windowsHandler;

        [Inject] private CameraController _cameraController;

        private void Update()
        {
            if (Input.GetButtonDown(INVENTORY_BUTTON_NAME))
            {
                _windowsHandler.SwitchWindowState("InventoryWindow");
            }

            HandleCursorAltState();
        }

        private void HandleCursorAltState()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _cameraController.SetBlocked(true);
                _cursorController.ShowCursor();
            }

            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                _cameraController.SetBlocked(false);
                _cursorController.HideCursor();
            }
        }
    }
}

