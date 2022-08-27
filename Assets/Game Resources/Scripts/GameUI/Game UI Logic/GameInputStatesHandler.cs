using GameUI.Logic;
using UnityEngine;
using Zenject;

public class GameInputStatesHandler : MonoBehaviour
{
    [SerializeField] private GameWindowsHandler _windowsHandler;
    [SerializeField] private CursorController _cursorController;

    [Inject] private CameraController _cameraController;
    [Inject] private PlayerUnit _player;
    private PlayerInputHandler _inputHandler => _player.InputHandler;

    private void Awake()
    {
        _windowsHandler.OnWindowStateChanged += OnWindowStateChanged;
    }

    private void OnDestroy()
    {
        _windowsHandler.OnWindowStateChanged -= OnWindowStateChanged;
    }

    private void OnWindowStateChanged(GameWindow window, bool isEnabled)
    {
        _cursorController.SetState(isEnabled && window.IsCursorVisible);
        _inputHandler.SetBlocked(isEnabled && window.IsBlockingPlayerInput);
        _cameraController.SetBlocked(isEnabled && window.IsBlockingPlayerInput);
    }
}
