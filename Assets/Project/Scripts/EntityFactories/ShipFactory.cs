using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Ship;
using Zenject;

namespace Project.Scripts.EntityFactories
{
    public class ShipFactory : EntityFactory<Ship, ShipPhysics>
    {
        private readonly float _acceleration;
        private readonly float _damping;
        private readonly float _rotationSpeed;

        public ShipFactory(
            Ship prefab,
            DiContainer container,
            PhysicsSystem physicsSystem,
            float acceleration,
            float damping,
            float rotationSpeed)
            : base(prefab, container, physicsSystem)
        {
            _acceleration = acceleration;
            _damping = damping;
            _rotationSpeed = rotationSpeed;
        }
        
        protected override ShipPhysics CreatePhysics(Ship entity)
        {
            var physics = Container.Instantiate<ShipPhysics>(new object[]
            {
                entity.transform,
                _acceleration,
                _damping,
                _rotationSpeed
            });
            Container.BindInstance(physics).AsTransient();
            return physics;
        }
    }
}