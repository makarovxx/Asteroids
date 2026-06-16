using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Enemies.Ufo;
using Zenject;

namespace Project.Scripts.EntityFactories
{
    public class UfoFactory : EntityFactory<Ufo, FollowPhysics>
    {
        private readonly ShipPhysicsProvider _shipPhysicsProvider;
        private readonly float               _speed;

        public UfoFactory(
            Ufo                  prefab,
            DiContainer          container,
            PhysicsSystem        physicsSystem,
            ShipPhysicsProvider  shipPhysicsProvider,
            float                speed)
            : base(prefab, container, physicsSystem)
        {
            _shipPhysicsProvider = shipPhysicsProvider;
            _speed               = speed;
        }
        
        protected override FollowPhysics CreatePhysics(Ufo entity)
        {
            return Container.Instantiate<FollowPhysics>(new object[]
            {
                entity.transform,
                _speed
            });
        }
    }
}