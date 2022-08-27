using UnityEngine;

namespace Zenject.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraController _cameraControllerPrefab;

        public override void InstallBindings()
        {
            var controllerInstance = Container.InstantiatePrefabForComponent<CameraController>(_cameraControllerPrefab);
            Container.Bind<CameraController>().FromInstance(controllerInstance).AsSingle();
        }
    }
}
