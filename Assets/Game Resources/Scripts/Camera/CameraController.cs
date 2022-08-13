using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraFovController _fovControler;
    [SerializeField] private CinemachineVirtualCamera _camera;

    public CameraFovController FovController => _fovControler;
    public CinemachineVirtualCamera CurrentCamera => _camera;
}
