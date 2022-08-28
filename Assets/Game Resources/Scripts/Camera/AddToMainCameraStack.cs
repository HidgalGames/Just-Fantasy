using UnityEngine;
using Zenject;

public class AddToMainCameraStack : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Inject] private CameraController _cameraController;

    private void Start()
    {
        _cameraController.AddOverlayCameraToStack(_camera);
    }
}
