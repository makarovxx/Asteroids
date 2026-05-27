using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
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
            Container.Bind<DesktopInput>().AsSingle();
            Container.Bind<RotationResolver>().AsSingle();
            Container.Bind<ShipPhysics>().AsSingle().WithArguments(_playerConfig.Body,
                _playerConfig.Acceleration, _playerConfig.Damping, _playerConfig.RotationSpeed);

            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WorldBoundsTeleport>().AsSingle();
            Container.BindInterfacesAndSelfTo<PhysicsSystem>()
                .AsSingle();
            
            RegisterShipPhysics();
        }
        
        private void RegisterShipPhysics()
        {
            PhysicsSystem physicsSystem =
                Container.Resolve<PhysicsSystem>();

            ShipPhysics shipPhysics =
                Container.Resolve<ShipPhysics>();

            physicsSystem.Register(shipPhysics);
        }
    }
}