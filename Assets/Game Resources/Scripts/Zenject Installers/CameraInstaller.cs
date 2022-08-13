using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private CameraController _cameraInstance;

    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(_cameraInstance).AsSingle();
    }
}