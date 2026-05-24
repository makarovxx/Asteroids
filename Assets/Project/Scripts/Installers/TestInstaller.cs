using System;
using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Core.Enemy.Factories;
using Project.Scripts.Entities;
using Project.Scripts.InputManageSystem;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers
{
    public class TestInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Transform _asteroidsContainer;
        [SerializeField] private int _asteroidsPoolSize = 10;

        public override void InstallBindings()
        {
            Container.Bind<DesktopInput>().AsSingle();
            Container.Bind<RotationResolver>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipPhysics>().AsSingle().WithArguments(_playerConfig.Body, _playerConfig.Velocity,
                _playerConfig.Acceleration, _playerConfig.Damping, _playerConfig.RotationSpeed);

            Container.Bind<SolidPhysics>().FromMethod(CreateSolidPhysics).AsTransient();
            Container.Bind<Asteroid>().FromComponentInNewPrefab(_asteroidPrefab).AsTransient();
            Container.Bind<Plugins.IFactory<Asteroid>>().To<AsteroidFactory>().AsSingle();
            Container.Bind<IPool<Asteroid>>().To<ObjectPool<Asteroid>>().AsSingle().WithArguments(_asteroidsPoolSize, _asteroidsContainer).NonLazy();
        }

        private SolidPhysics CreateSolidPhysics(InjectContext context)
        {
            if (context.ObjectInstance is Asteroid asteroid)
                return new SolidPhysics(asteroid.transform, context.Container.Resolve<RotationResolver>());

            throw new InvalidOperationException($"{nameof(SolidPhysics)} can be created only for {nameof(Asteroid)} instances.");
        }
    }
}
