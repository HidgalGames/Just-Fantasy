using UnityEngine;

namespace Zenject.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerUnit _playerPrefab;

        public override void InstallBindings()
        {
            var playerInstance = Container.InstantiatePrefabForComponent<PlayerUnit>(_playerPrefab);

            Container.Bind<PlayerUnit>().FromInstance(playerInstance).AsSingle().NonLazy();

            playerInstance.transform.SetParent(null);
        }
    }
}