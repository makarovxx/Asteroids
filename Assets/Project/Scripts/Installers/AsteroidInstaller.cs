using Project.Scripts.Core.Enemy.Factories;
using Project.Scripts.Entities;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Transform _asteroidsContainer;
        [SerializeField] private int _poolSize = 10;

        public override void InstallBindings()
        {
            Container.Bind<ICreator<Asteroid>>().To<AsteroidFactory>().AsSingle().WithArguments(_asteroidPrefab);

            Container.Bind<IPool<Asteroid>>().To<ObjectPool<Asteroid>>().AsSingle()
                .WithArguments(_poolSize, _asteroidsContainer);
        }
    }
}