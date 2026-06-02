using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Ship;
using Project.Scripts.EntityFactories;
using Project.Scripts.InputManageSystem;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<DesktopInput>().AsSingle();
            Container.Bind<RotationResolver>().AsSingle();

            BindPhysicsSystem();
            BindShipSpawner();
        }

        private void BindPhysicsSystem()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WorldBoundsTeleport>().AsSingle();
            Container.BindInterfacesAndSelfTo<PhysicsSystem>().AsSingle();
        }

        private void BindShipSpawner()
        {
            Container.Bind<ICreator<Ship>>()
                .To<ShipFactory>()
                .AsSingle()
                .WithArguments(
                    _playerConfig.Prefab,
                    _playerConfig.Acceleration,
                    _playerConfig.Damping,
                    _playerConfig.RotationSpeed);

            Container.BindInterfacesTo<ShipSpawner>()
                .AsSingle()
                .WithArguments(_playerConfig);

            Container.BindInterfacesAndSelfTo<Ship>().FromComponentInHierarchy().AsSingle();
        }
    }
}
