using System;
using Cysharp.Threading.Tasks;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Projectile;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.InputManageSystem;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.Weapons
{
    public class BulletLauncher : IInitializable, IDisposable, ITickable
    {
        private const float OffsetFirePosition = 0.3f;
        private readonly IPool<Bullet> _bulletPool;
        private readonly SignalBus _signalBus;
        private readonly DesktopInput _input;
        private readonly BulletData _bulletData;
        private readonly ShipPhysicsProvider _shipPhysics;
        private Vector2 _positionFire;

        [Inject]
        public BulletLauncher(BulletData bulletData, ShipPhysicsProvider shipPhysics, IPool<Bullet> bulletPool,
            DesktopInput input, SignalBus signalBus)
        {
            _bulletData = bulletData;
            _shipPhysics = shipPhysics;
            _bulletPool = bulletPool;
            _input = input;
            _signalBus = signalBus;
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
                FireBullet(_shipPhysics.PhysicsTarget.DirectionBody, GetSetPositionFire());
            }
        }

        private void FireBullet(Vector2 direction, Vector2 positionFire)
        {
            if (!_bulletPool.TryGetObject(out Bullet bullet))
                return;

            var bulletPhys = bullet.Physics;
            bulletPhys.Position = positionFire;
            bulletPhys.SetVelocity(direction * _bulletData.Speed);

            BulletLifeRoutine(bullet).Forget();
        }

        private async UniTask BulletLifeRoutine(Bullet bullet)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_bulletData.LifeTime),
                cancellationToken: bullet.LifeTimeCts.Token);

            DespawnBullet(bullet);
        }

        private void DespawnBullet(EnemyHitByWeaponSignal signal)
        {
            if (signal.HitBy is Bullet bullet)
            {
                DespawnBullet(bullet);
            }
        }

        private void DespawnBullet(Bullet bullet)
        {
            bullet.Physics.StopMove();

            _bulletPool.PushObject(bullet);
        }

        private Vector2 GetSetPositionFire()
        {
            return _shipPhysics.PhysicsTarget.Position + _shipPhysics.PhysicsTarget.DirectionBody * OffsetFirePosition;
        }
    }
}