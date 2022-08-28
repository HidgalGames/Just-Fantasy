using GameUI.Logic;
using UnityEngine;

public class ResetRenderRotationByWindowState : MonoBehaviour
{
    [SerializeField] private GameWindow _window;
    [SerializeField] private RotateRenderedObject _targetRotation;

    private void Awake()
    {
        _window.OnWindowStateChanged += OnWindowStateChanged;
    }

    private void OnDestroy()
    {
        _window.OnWindowStateChanged -= OnWindowStateChanged;
    }

    private void OnWindowStateChanged(GameWindow window, bool isOpened)
    {
        if (!isOpened) return;

        _targetRotation.ResetRotation();
    }
}
