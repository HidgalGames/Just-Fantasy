using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraFovController _fovControler;
    [SerializeField] private CinemachineFreeLook _thirdPersonCamera;

    public CameraFovController FovController => _fovControler;
    public CinemachineFreeLook ThirdPersonCamera => _thirdPersonCamera;
}
