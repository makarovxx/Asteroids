using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Ship;
using UnityEngine;
using Zenject;

namespace Project.Scripts.EntityFactories
{
    public class ShipSpawner : IInitializable
    {
        private readonly PlayerConfig _playerConfig;
        private readonly PhysicsSystem _physicsSystem;
        private readonly ShipPhysicsProvider _shipPhysicsProvider;
        private readonly DiContainer _container;
        private readonly SignalBus _signalBus;

        [Inject]
        public ShipSpawner(
            PlayerConfig playerConfig,
            PhysicsSystem physicsSystem,
            ShipPhysicsProvider shipPhysicsProvider,
            DiContainer container, SignalBus signalBus)
        {
            _playerConfig = playerConfig;
            _physicsSystem = physicsSystem;
            _shipPhysicsProvider = shipPhysicsProvider;
            _container = container;
            _signalBus = signalBus;
            Create();
        }

        void IInitializable.Initialize()
        {
            // Create();
        }

        private void Create()
        {
            Ship ship = _container.InstantiatePrefabForComponent<Ship>(
                _playerConfig.Prefab,
                _playerConfig.SpawnPoint,
                Quaternion.identity,
                _playerConfig.Container);


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
                _playerConfig.Acceleration,
                _playerConfig.Damping,
                _playerConfig.RotationSpeed
            });
        }
    }
}