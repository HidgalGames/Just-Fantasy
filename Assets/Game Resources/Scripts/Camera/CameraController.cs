using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraFovController _fovControler;
    [SerializeField] private CinemachineFreeLook _thirdPersonCamera;

    [Inject] private PlayerUnit _player;

    public CameraFovController FovController => _fovControler;
    public CinemachineFreeLook ThirdPersonCamera => _thirdPersonCamera;

    private void Awake()
    {
        ThirdPersonCamera.Follow = _player.transform;
        ThirdPersonCamera.LookAt = _player.transform;
    }
}
