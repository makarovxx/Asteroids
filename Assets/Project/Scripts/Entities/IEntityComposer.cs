using System;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.InputManageSystem;
using Project.Scripts.Plugins;
using Zenject;

namespace Project.Scripts.Entities
{
    public interface IEntityComposer
    {
        
    }

    public interface IEntityComposer<T> : IEntityComposer
    {
        
    }
    
    public abstract class EntityBinder
    {
        protected PhysicsSystem PhysicsSystem;
        protected RotationResolver RotationResolver;
        
        protected EntityBinder(PhysicsSystem physicsSystem, RotationResolver rotationResolver)
        {
            PhysicsSystem = physicsSystem;
            RotationResolver = rotationResolver;
        }
        
        public abstract void Bind();
    }
    
    public class AsteroidBinder : EntityBinder
    {
        private ICreator<Asteroid> _asteroidCreator;

        public AsteroidBinder(PhysicsSystem physicsSystem, RotationResolver rotationResolver, ICreator<Asteroid> asteroidCreator) : base(physicsSystem, rotationResolver)
        {
            _asteroidCreator = asteroidCreator;
        }

        public override void Bind()
        {
            var asteroid = _asteroidCreator.Create();
            
            SolidPhysics physics = new SolidPhysics(asteroid.transform, RotationResolver);

            PhysicsSystem.Register(physics);

            asteroid.Initialize(physics);
        }
    }
}