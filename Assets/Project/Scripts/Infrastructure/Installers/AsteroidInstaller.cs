using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.EnemyLifeCycle;
using Project.Scripts.Gameplay.Entities.Enemies.Asteroids;
using Project.Scripts.Infrastructure.Configs.PersistantData;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Infrastructure.EntityFactories;
using Project.Scripts.Plugins;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class AsteroidInstaller : MonoInstaller
    {
        [ReadOnly] [SerializeField] private AsteroidsPersistantData _persistantData;
        [Inject] [ReadOnly] [SerializeField] private AsteroidsData _asteroidsData;

        public override void InstallBindings()
        {
            BindLargeAsteroid();
            BindMediumAsteroid();
            BindSmallAsteroid();

            Container.BindInterfacesTo<AsteroidLifeCycleController>()
                .AsSingle()
                .WithArguments(_asteroidsData);
        }

        private void BindLargeAsteroid()
        {
            Container
                .Bind<ICreator<LargeAsteroid>>()
                .To<EntityFactory<LargeAsteroid, SolidPhysics>>()
                .AsSingle()
                .WithArguments(_persistantData.PrefabLargeAsteroid);

            Container
                .Bind<IPool<LargeAsteroid>>()
                .To<ObjectPool<LargeAsteroid>>()
                .AsSingle()
                .WithArguments(_asteroidsData.PoolSizeLarge, _persistantData.AsteroidsContainer)
                .NonLazy();
        }

        private void BindMediumAsteroid()
        {
            Container
                .Bind<ICreator<MediumAsteroid>>()
                .To<EntityFactory<MediumAsteroid, SolidPhysics>>()
                .AsSingle()
                .WithArguments(_persistantData.PrefabMediumAsteroid);

            Container
                .Bind<IPool<MediumAsteroid>>()
                .To<ObjectPool<MediumAsteroid>>()
                .AsSingle()
                .WithArguments(_asteroidsData.PoolSizeMedium, _persistantData.AsteroidsContainer)
                .NonLazy();
        }

        private void BindSmallAsteroid()
        {
            Container
                .Bind<ICreator<SmallAsteroid>>()
                .To<EntityFactory<SmallAsteroid, SolidPhysics>>()
                .AsSingle()
                .WithArguments(_persistantData.PrefabSmallAsteroid);

            Container
                .Bind<IPool<SmallAsteroid>>()
                .To<ObjectPool<SmallAsteroid>>()
                .AsSingle()
                .WithArguments(_asteroidsData.PoolSizeSmall, _persistantData.AsteroidsContainer)
                .NonLazy();
        }
    }
}