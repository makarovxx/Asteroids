using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities;
using Project.Scripts.Gameplay.Utilities.World;
using Zenject;

namespace Project.Scripts.Infrastructure.EntityFactories
{
    public class EntityFactory<TEntity, TPhysics> : IEntityFactory<TEntity>
        where TEntity : Entity<TPhysics>
        where TPhysics : PhysicBase
    {
        private readonly TEntity _prefab;
        private readonly PhysicsSystem _physicsSystem;
        protected readonly DiContainer Container;

        protected EntityFactory(TEntity prefab, DiContainer container, PhysicsSystem physicsSystem)
        {
            _prefab = prefab;
            Container = container;
            _physicsSystem = physicsSystem;
        }

        public TEntity Create()
        {
            TEntity entity = Container.InstantiatePrefabForComponent<TEntity>(_prefab);

            TPhysics physics = CreatePhysics(entity);
            _physicsSystem.Register(physics);
            entity.Init(physics);

            return entity;
        }

        protected virtual TPhysics CreatePhysics(TEntity entity)
        {
            return Container.Instantiate<TPhysics>(new object[] { entity.transform });
        }
    }
}