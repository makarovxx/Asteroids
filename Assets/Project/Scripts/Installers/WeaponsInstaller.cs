using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Projectile;
using Project.Scripts.EntityFactories;
using Project.Scripts.Plugins;
using Project.Scripts.Weapons;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers
{
    public class WeaponsInstaller : MonoInstaller
    {
        [SerializeField] private WeaponsConfig _weaponsConfig;

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
                .WithArguments(_weaponsConfig.BulletPrefab);

            Container.Bind<IPool<Bullet>>().To<ObjectPool<Bullet>>().AsSingle()
                .WithArguments(_weaponsConfig.PoolSizeBullets, _weaponsConfig.BulletContainer);

            Container.BindInterfacesAndSelfTo<BulletLauncher>().AsSingle();
        }
    }
}