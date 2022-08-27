using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Blockable _blockable;
    [SerializeField] private CameraFovController _fovControler;
    [SerializeField] private CinemachineFreeLook _thirdPersonCamera;

    [Inject] private PlayerUnit _player;

    public bool IsBlocked => _blockable.IsBlocked;
    public CameraFovController FovController => _fovControler;
    public CinemachineFreeLook ThirdPersonCamera => _thirdPersonCamera;

    private void Awake()
    {
        ThirdPersonCamera.Follow = _player.transform;
        ThirdPersonCamera.LookAt = _player.transform;
    }

    public void SetBlocked(bool isBlocked)
    {
        _blockable.SetBlocked(isBlocked);

        _thirdPersonCamera.gameObject.SetActive(!IsBlocked);
    }
}
