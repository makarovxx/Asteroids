using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities;
using Project.Scripts.Entities.Enemies.Asteroids;
using Project.Scripts.Plugins;
using Zenject;

namespace Project.Scripts.EntityFactories
{
    public class EntityFactory<TEntity, TPhysics> : IEntityFactory<TEntity>
        where TEntity : Entity<TPhysics>
        where TPhysics : PhysicBase
    {
        private readonly TEntity _prefab;
        private readonly PhysicsSystem _physicsSystem;
        protected readonly DiContainer Container;

        public EntityFactory(TEntity prefab, DiContainer container, PhysicsSystem physicsSystem)
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