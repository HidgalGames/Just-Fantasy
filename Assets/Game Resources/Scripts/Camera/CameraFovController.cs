using UnityEngine;

using Zenject;

public class CameraFovController : MonoBehaviour
{
    [SerializeField] private float _baseFOV = 60f;
    [SerializeField] private float _maxFOV = 70f;
    [Space]
    [SerializeField] private float _fovChangeSpeed = 10f;

    [Inject] CameraController _cameraController;

    private MoveController _moveController;
    private float _targetFov;

    public void SetupMoveController(MoveController controller)
    {
        _moveController = controller;
    }

    private void LateUpdate()
    {
        if (!_moveController) return;

        _targetFov = Mathf.Clamp(_baseFOV * _moveController.CurrentMoveSpeed / _moveController.BaseMoveSpeed, _baseFOV, _maxFOV);
        _cameraController.CurrentCamera.m_Lens.FieldOfView = Mathf.Lerp(_cameraController.CurrentCamera.m_Lens.FieldOfView, _targetFov, _fovChangeSpeed * Time.smoothDeltaTime);
    }
}
