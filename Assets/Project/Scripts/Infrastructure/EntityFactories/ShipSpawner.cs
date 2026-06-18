using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Ship;
using Project.Scripts.Gameplay.Utilities.World;
using Project.Scripts.Infrastructure.Configs.PersistantData;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Zenject;

namespace Project.Scripts.Infrastructure.EntityFactories
{
    public sealed class ShipSpawner
    {
        private readonly PlayerPersistantData _playerConfig;
        private readonly PlayerData _playerData;
        private readonly PhysicsSystem _physicsSystem;
        private readonly ShipPhysicsProvider _shipPhysicsProvider;
        private readonly DiContainer _container;
        private readonly SignalBus _signalBus;
        [Inject] private readonly DisposableManager _disposableManager;

        [Inject]
        public ShipSpawner(PlayerPersistantData playerConfig,
            PlayerData playerData,
            PhysicsSystem physicsSystem,
            ShipPhysicsProvider shipPhysicsProvider,
            DiContainer container, SignalBus signalBus)
        {
            _playerConfig = playerConfig;
            _playerData = playerData;
            _physicsSystem = physicsSystem;
            _shipPhysicsProvider = shipPhysicsProvider;
            _container = container;
            _signalBus = signalBus;
            Create();
        }

        private void Create()
        {
            Ship ship = _container.InstantiatePrefabForComponent<Ship>(_playerConfig.Prefab, _playerConfig.Container);


            ShipPhysics physics = CreatePhysics(ship);
            
            _physicsSystem.Register(physics);
            _shipPhysicsProvider.Init(physics);
            ship.Init(physics);
        }

        private ShipPhysics CreatePhysics(Ship entity)
        {
            return _container.Instantiate<ShipPhysics>(new object[]
            {
                entity.transform,
                _playerData.Acceleration,
                _playerData.Damping,
                _playerData.RotationSpeed
            });
        }
    }
}