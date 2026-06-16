using System;
using Cysharp.Threading.Tasks;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities.Projectile;
using Project.Scripts.InputManageSystem;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Weapons
{
    public class BulletLauncher : IInitializable, IDisposable, ITickable
    {
        private readonly IPool<Bullet> _bulletPool;
        private readonly SignalBus _signalBus;
        private readonly DesktopInput _input;
        private readonly ShipPhysicsProvider _shipPhysics;
        
        private const float BulletLifeTime = 3f;
        
        [Inject]
        public BulletLauncher(IPool<Bullet> bulletPool, SignalBus signalBus, ShipPhysicsProvider shipPhysics, DesktopInput input)
        {
            _bulletPool = bulletPool;
            _signalBus = signalBus;
            _shipPhysics = shipPhysics;
            _input = input;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyHitByWeaponSignal>(DespawnBullet);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyHitByWeaponSignal>(DespawnBullet);
        }

        public void Tick()
        {
            if (_input.IsFireBulletLauncher())
            {
                Debug.Log("FireBullet");
                FireBullet(_shipPhysics.PhysicsTarget.DirectionBodyDefault, _shipPhysics.PhysicsTarget.Position);
            }
        }

        private void FireBullet(Vector2 direction, Vector2 positionFire)
        {
            if (!_bulletPool.TryGetObject(out Bullet bullet))
                return;

            var bulletPhys = bullet.Physics;
            bulletPhys.SetVelocity(direction * 6);
            bulletPhys.Position = positionFire;
            
            BulletLifeRoutine(bullet).Forget();
        }

        private async UniTask BulletLifeRoutine(Bullet bullet)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(BulletLifeTime));

            if (bullet.Physics.IsActive)
            {
                bullet.Physics.StopMove();
                _bulletPool.PushObject(bullet);
            }
        }

        private void DespawnBullet(EnemyHitByWeaponSignal signal)
        {
            if (signal.HitBy is Bullet bullet)
            {
                _bulletPool.PushObject(bullet);
                bullet.Physics.StopMove();
            }
        }
    }
}