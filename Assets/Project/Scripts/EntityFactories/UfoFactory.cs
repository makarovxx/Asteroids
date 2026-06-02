using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Enemies.Ufo;
using Zenject;

namespace Project.Scripts.EntityFactories
{
    public class UfoFactory : EntityFactory<Ufo, FollowPhysics>
    {
        private readonly IPhysics _shipTarget;
        private readonly float _speed;
        
        public UfoFactory(Ufo prefab, DiContainer container, PhysicsSystem physicsSystem, IPhysics shipTarget,
            float speed) : base(prefab, container, physicsSystem)
        {
            _shipTarget = shipTarget;
            _speed = speed;
        }

        protected override FollowPhysics CreatePhysics(Ufo entity)
        {
            return Container.Instantiate<FollowPhysics>(new object[]
            {
                entity.transform,
                _shipTarget,
                _speed
            });
        }
    }
}