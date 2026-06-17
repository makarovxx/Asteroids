using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Projectile;
using Project.Scripts.Gameplay.Utilities.Weapons;
using Project.Scripts.Infrastructure.Configs.PersistantData;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Infrastructure.EntityFactories;
using Project.Scripts.Plugins;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class WeaponsInstaller : MonoInstaller
    {
        [SerializeField] private WeaponsPersistantData _persistantData;
        [Inject] [ShowInInspector,ReadOnly] private BulletData _bulletData;

        public override void InstallBindings()
        {
            BindBulletLauncher();
        }

        private void BindBulletLauncher()
        {
            Container
                .Bind<ICreator<Bullet>>()
                .To<EntityFactory<Bullet, SolidPhysics>>()
                .AsSingle()
                .WithArguments(_persistantData.BulletPrefab);

            Container.Bind<IPool<Bullet>>().To<ObjectPool<Bullet>>().AsSingle()
                .WithArguments(_bulletData.PoolSize, _persistantData.BulletContainer);

            Container.BindInterfacesAndSelfTo<BulletLauncher>().AsSingle().WithArguments(_bulletData);
        }
    }
}