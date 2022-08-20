using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerUnit _playerPrefab;

    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefabForComponent<PlayerUnit>(_playerPrefab);

        Container.Bind<PlayerUnit>().FromInstance(player).AsSingle().NonLazy();
    }
}