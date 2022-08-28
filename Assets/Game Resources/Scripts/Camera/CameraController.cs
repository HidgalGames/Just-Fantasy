using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Blockable _blockable;
    [SerializeField] private CameraFovController _fovControler;
    [Header("Camera Components")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineFreeLook _thirdPersonCamera;

    [Inject] private PlayerUnit _player;

    private UniversalAdditionalCameraData _mainCameraData;

    public bool IsBlocked => _blockable.IsBlocked;
    public CameraFovController FovController => _fovControler;
    public CinemachineFreeLook ThirdPersonCamera => _thirdPersonCamera;

    private void Awake()
    {
        ThirdPersonCamera.Follow = _player.transform;
        ThirdPersonCamera.LookAt = _player.transform;

        _mainCameraData = _mainCamera.GetUniversalAdditionalCameraData();
    }

    public void SetBlocked(bool isBlocked)
    {
        _blockable.SetBlocked(isBlocked);

        _thirdPersonCamera.gameObject.SetActive(!IsBlocked);
    }

    public void AddOverlayCameraToStack(Camera camera)
    {
        _mainCameraData.cameraStack.Add(camera);
    }

    public void RemoveOverlayCameraFromStack(Camera camera)
    {
        _mainCameraData.cameraStack.Remove(camera);
    }
}
