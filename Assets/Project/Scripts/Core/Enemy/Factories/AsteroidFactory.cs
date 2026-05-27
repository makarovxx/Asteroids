using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities;
using Project.Scripts.Plugins;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Factories
{
    public class AsteroidFactory : ICreator<Asteroid>
    {
        private readonly Asteroid _prefab;

        private readonly RotationResolver _rotationResolver;

        private readonly PhysicsSystem _physicsSystem;

        public AsteroidFactory(Asteroid prefab, RotationResolver rotationResolver,
            PhysicsSystem physicsSystem)
        {
            _prefab = prefab;
            _rotationResolver = rotationResolver;
            _physicsSystem = physicsSystem;
        }

        public Asteroid Create()
        {
            Asteroid asteroid = Object.Instantiate(_prefab);

            SolidPhysics physics = new SolidPhysics(asteroid.transform, _rotationResolver);

            _physicsSystem.Register(physics);

            asteroid.Initialize(physics);

            return asteroid;
        }
    }
}