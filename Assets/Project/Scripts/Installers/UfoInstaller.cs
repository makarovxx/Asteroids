using Project.Scripts.Configs;
using Project.Scripts.EnemyLifeCycle;
using Project.Scripts.Entities.Enemies.Ufo;
using Project.Scripts.EntityFactories;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;
 
namespace Project.Scripts.Installers
{
    public class UfoInstaller : MonoInstaller
    {
        [SerializeField] private UfoConfig _config;
 
        public override void InstallBindings()
        {
            BindUfoFactory();
            BindUfoPool();
            BindUfoLifeCycle();
        }
 
        private void BindUfoFactory()
        {
            Container
                .Bind<ICreator<Ufo>>()
                .To<UfoFactory>()
                .AsSingle()
                .WithArguments(_config.Prefab, _config.Speed);
        }
 
        private void BindUfoPool()
        {
            Container
                .Bind<IPool<Ufo>>()
                .To<ObjectPool<Ufo>>()
                .AsSingle()
                .WithArguments(_config.PoolSize, _config.Container)
                .NonLazy();
        }
 
        private void BindUfoLifeCycle()
        {
            Container
                .BindInterfacesTo<UfoLifeCycleController>()
                .AsSingle()
                .WithArguments(_config);
        }
    }
}