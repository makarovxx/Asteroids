using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Enemies.Ufo;
using Project.Scripts.Gameplay.Utilities.World;
using Zenject;

namespace Project.Scripts.Infrastructure.EntityFactories
{
    public sealed class UfoFactory : EntityFactory<Ufo, FollowPhysics>
    {
        private readonly float _speed;

        public UfoFactory(
            Ufo prefab,
            DiContainer container,
            PhysicsSystem physicsSystem,
            float speed)
            : base(prefab, container, physicsSystem)
        {
            _speed = speed;
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