using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Ship;
using Project.Scripts.Gameplay.ShipStateMachine;
using Project.Scripts.Infrastructure.Configs.PersistantData;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Infrastructure.EntityFactories;
using Project.Scripts.InputManageSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public sealed class ShipInstaller : MonoInstaller
    {
        [ReadOnly] [SerializeField] private ShipPersistantData _shipPersistantData;
        [Inject] [ReadOnly] [SerializeField] private ShipData _shipData;

        public override void InstallBindings()
        {
            BindInputSystem();
            Container.Bind<RotationResolver>().AsSingle();
            Container.Bind<ShipProvider>().AsSingle();
            Container.Bind<ShipPhysicsProvider>().AsSingle();

            BindShipSpawner();
            BindShipStateMachine();
        }

        private void BindInputSystem()
        {
            Container.Bind<MobileInputState>().AsSingle();
            Container.Bind<IInputStrategy>().To<InputDesktopStrategy>().AsSingle();
            Container.Bind<IInputStrategy>().To<InputMobileStrategy>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputDetector>().AsSingle();
            Container.Bind<InputManager>().AsSingle();
        }

        private void BindShipSpawner()
        {
            Container.BindInterfacesAndSelfTo<ShipSpawner>().AsSingle()
                .WithArguments(_shipPersistantData).NonLazy();
        }

        private void BindShipStateMachine()
        {
            Container.BindInterfacesAndSelfTo<InvulnerabilitySystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<VulnerableState>().AsSingle();

            Container.BindInterfacesAndSelfTo<InvulnerableState>().AsSingle();

            Container.BindInterfacesAndSelfTo<DeadState>().AsSingle();

            Container.BindInterfacesAndSelfTo<ShipStateMachine>().AsSingle();

            Container.BindInterfacesAndSelfTo<ShipStateMachineBehaviour>().AsSingle().NonLazy();
        }
    }
}