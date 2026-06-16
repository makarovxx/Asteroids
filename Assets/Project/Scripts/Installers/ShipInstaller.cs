using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Ship;
using Project.Scripts.EntityFactories;
using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.Bind<DesktopInput>().AsSingle().NonLazy();
            Container.Bind<RotationResolver>().AsSingle().NonLazy();

            BindShipSpawner();
            Container.Bind<ShipPhysicsProvider>().AsSingle();
        }

        

        private void BindShipSpawner()
        {
            Container.BindInterfacesAndSelfTo<ShipSpawner>()
                .AsSingle()
                .WithArguments(_playerConfig).NonLazy();
            Container.BindInterfacesTo<Ship>().FromComponentInHierarchy().AsSingle();
        }
    }
}