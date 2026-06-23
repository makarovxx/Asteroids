using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Ship;
using Project.Scripts.Gameplay.Entities.Projectile;
using Project.Scripts.Gameplay.Utilities.World;
using Project.Scripts.Infrastructure.Configs.PersistantData;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Zenject;

namespace Project.Scripts.Infrastructure.EntityFactories
{
    public sealed class ShipSpawner
    {
        private readonly ShipPersistantData _shipConfig;
        private readonly ShipData _shipData;
        private readonly PhysicsSystem _physicsSystem;
        private readonly ShipPhysicsProvider _shipPhysicsProvider;
        private readonly ShipProvider _shipProvider;
        private readonly DiContainer _container;

        [Inject]
        public ShipSpawner(ShipPersistantData shipConfig,
            ShipData shipData,
            PhysicsSystem physicsSystem,
            ShipPhysicsProvider shipPhysicsProvider,
            ShipProvider shipProvider,
            DiContainer container)
        {
            _shipConfig = shipConfig;
            _shipData = shipData;
            _physicsSystem = physicsSystem;
            _shipPhysicsProvider = shipPhysicsProvider;
            _shipProvider = shipProvider;
            _container = container;
            Create();
        }

        private void Create()
        {
            Ship ship = _container.InstantiatePrefabForComponent<Ship>(_shipConfig.Prefab, _shipConfig.Container);

            ShipPhysics physics = CreatePhysics(ship);

            _physicsSystem.Register(physics);
            _shipPhysicsProvider.Init(physics);
            _shipProvider.Init(ship, physics);
            ship.Init(physics);
        }

        private ShipPhysics CreatePhysics(Ship entity)
        {
            return _container.Instantiate<ShipPhysics>(new object[]
            {
                entity.transform,
                _shipData.Acceleration,
                _shipData.Damping,
                _shipData.RotationSpeed
            });
        }
    }
}