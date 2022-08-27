using UnityEngine;

using Zenject;

public class CameraFovController : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [Space]
    [Header("Settings")]
    [SerializeField] private float _baseFOV = 60f;
    [SerializeField] private float _maxFOV = 70f;
    [Space]
    [SerializeField] private float _fovChangeSpeed = 10f;

    [Inject] private PlayerUnit _player;

    private float _targetFov;

    private MoveController _moveController => _player.MoveController;

    private void Awake()
    {
        if (!_cameraController) _cameraController = GetComponent<CameraController>();
    }

    private void LateUpdate()
    {
        if (!_cameraController) return;
        if (!_moveController) return;

        _targetFov = Mathf.Clamp(_baseFOV * _moveController.CurrentMoveSpeed / _moveController.BaseMoveSpeed, _baseFOV, _maxFOV);
        _cameraController.ThirdPersonCamera.m_Lens.FieldOfView = Mathf.Lerp(_cameraController.ThirdPersonCamera.m_Lens.FieldOfView, _targetFov, _fovChangeSpeed * Time.smoothDeltaTime);
    }
}
