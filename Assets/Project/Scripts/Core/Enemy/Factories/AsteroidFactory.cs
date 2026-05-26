using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities;
using Project.Scripts.Plugins;
using Zenject;

namespace Project.Scripts.Core.Enemy.Factories
{
    public class AsteroidFactory : ICreator<Asteroid>
    {
        private readonly DiContainer _container;

        private readonly Asteroid _prefab;

        private readonly RotationResolver _rotationResolver;

        private readonly PhysicsSystem _physicsSystem;

        public AsteroidFactory(DiContainer container, Asteroid prefab, RotationResolver rotationResolver,
            PhysicsSystem physicsSystem)
        {
            _container = container;
            _prefab = prefab;
            _rotationResolver = rotationResolver;
            _physicsSystem = physicsSystem;
        }

        public Asteroid Create()
        {
            Asteroid asteroid = _container.InstantiatePrefabForComponent<Asteroid>(_prefab);

            SolidPhysics physics = new SolidPhysics(asteroid.transform, _rotationResolver);

            _physicsSystem.Register(physics);

            asteroid.Initialize(physics);

            asteroid.gameObject.SetActive(false);

            return asteroid;
        }
    }
}