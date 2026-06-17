using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Ship;
using Project.Scripts.Infrastructure.Configs.PersistantData;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Infrastructure.EntityFactories;
using Project.Scripts.InputManageSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class ShipInstaller : MonoInstaller
    {
        [ReadOnly] [SerializeField] private PlayerPersistantData _playerConfig;
        [Inject] [ReadOnly] [SerializeField] private PlayerData _playerData;

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