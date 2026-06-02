using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.EnemyLifeCycle;
using Project.Scripts.Entities.Enemies.Asteroids;
using Project.Scripts.EntityFactories;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidsConfig _config;

        public override void InstallBindings()
        {
            BindLargeAsteroid();
            BindMediumAsteroid();
            BindSmallAsteroid();

            Container.BindInterfacesTo<AsteroidLifeCycleController>()
                .AsSingle()
                .WithArguments(_config);
        }

        private void BindLargeAsteroid()
        {
            Container
                .Bind<ICreator<LargeAsteroid>>()
                .To<EntityFactory<LargeAsteroid, SolidPhysics>>()
                .AsSingle()
                .WithArguments(_config.PrefabLargeAsteroid);

            Container
                .Bind<IPool<LargeAsteroid>>()
                .To<ObjectPool<LargeAsteroid>>()
                .AsSingle()
                .WithArguments(_config.PoolSizeLarge, _config.AsteroidsContainer)
                .NonLazy();
        }

        private void BindMediumAsteroid()
        {
            Container
                .Bind<ICreator<MediumAsteroid>>()
                .To<EntityFactory<MediumAsteroid, SolidPhysics>>()
                .AsSingle()
                .WithArguments(_config.PrefabMediumAsteroid);

            Container
                .Bind<IPool<MediumAsteroid>>()
                .To<ObjectPool<MediumAsteroid>>()
                .AsSingle()
                .WithArguments(_config.PoolSizeMedium, _config.AsteroidsContainer)
                .NonLazy();
        }

        private void BindSmallAsteroid()
        {
            Container
                .Bind<ICreator<SmallAsteroid>>()
                .To<EntityFactory<SmallAsteroid, SolidPhysics>>()
                .AsSingle()
                .WithArguments(_config.PrefabSmallAsteroid);

            Container
                .Bind<IPool<SmallAsteroid>>()
                .To<ObjectPool<SmallAsteroid>>()
                .AsSingle()
                .WithArguments(_config.PoolSizeSmall, _config.AsteroidsContainer)
                .NonLazy();
        }
    }
}