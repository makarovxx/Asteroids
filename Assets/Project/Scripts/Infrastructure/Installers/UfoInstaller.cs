using Project.Scripts.Gameplay.EnemyLifeCycle;
using Project.Scripts.Gameplay.Entities.Enemies.Ufo;
using Project.Scripts.Infrastructure.Configs.PersistantData;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Infrastructure.EntityFactories;
using Project.Scripts.Plugins;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class UfoInstaller : MonoInstaller
    {
        [ReadOnly] [SerializeField] private UfoPersistantData _persistantData;
        [Inject] [ReadOnly] [SerializeField] private UfoData _ufoData;
 
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
                .WithArguments(_persistantData.Prefab, _ufoData.Speed);
        }
 
        private void BindUfoPool()
        {
            Container
                .Bind<IPool<Ufo>>()
                .To<ObjectPool<Ufo>>()
                .AsSingle()
                .WithArguments(_ufoData.PoolSize, _persistantData.Container)
                .NonLazy();
        }
 
        private void BindUfoLifeCycle()
        {
            Container
                .BindInterfacesTo<UfoLifeCycleController>()
                .AsSingle()
                .WithArguments(_ufoData);
        }
    }
}