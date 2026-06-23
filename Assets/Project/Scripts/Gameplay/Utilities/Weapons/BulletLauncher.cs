using System;
using Cysharp.Threading.Tasks;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Core.TickableSystem;
using Project.Scripts.Gameplay.Entities.Projectile;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.InputManageSystem;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.Weapons
{
    public sealed class BulletLauncher : IInitializable, IDisposable, IBehaviourTickable
    {
        private const float OffsetFirePosition = 0.3f;
        
        private readonly IPool<Bullet> _bulletPool;
        private readonly SignalBus _signalBus;
        private readonly InputManager _input;
        private readonly ShipPhysicsProvider _shipPhysics;

        private readonly float _bulletSpeed;
        private readonly float _bulletLifeTime;
        private readonly float _cooldownFire;
        
        private float _cooldownTimer;

        [Inject]
        public BulletLauncher(BulletData bulletData, ShipPhysicsProvider shipPhysics, IPool<Bullet> bulletPool,
            InputManager input, SignalBus signalBus)
        {
            _shipPhysics = shipPhysics;
            _bulletPool = bulletPool;
            _input = input;
            _signalBus = signalBus;
            _cooldownTimer = bulletData.CooldownFire;
            _bulletSpeed = bulletData.Speed;
            _bulletLifeTime = bulletData.LifeTime;
            _cooldownFire = bulletData.CooldownFire;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<WeaponHitEnemy>(DespawnBullet);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<WeaponHitEnemy>(DespawnBullet);
        }

        public void Tick(float deltaTime)
        {
            _cooldownTimer -= deltaTime;
            if (!_input.IsFireBulletLauncher()) return;
            if (_cooldownTimer <= 0)
                FireBullet(_shipPhysics.PhysicsTarget.DirectionBody, GetPositionFire());
        }

        private void FireBullet(Vector2 direction, Vector2 positionFire)
        {
            if (!_bulletPool.TryGetObject(out Bullet bullet))
                return;

            var bulletPhys = bullet.Physics;
            bulletPhys.Position = positionFire;
            bulletPhys.SetVelocity(direction * _bulletSpeed);

            BulletLifeRoutine(bullet).Forget();
            ResetCooldownFire();
        }

        private async UniTask BulletLifeRoutine(Bullet bullet)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_bulletLifeTime),
                cancellationToken: bullet.LifeTimeCts.Token);

            DespawnBullet(bullet);
        }

        private void DespawnBullet(WeaponHitEnemy signal)
        {
            if (signal.HitBy is Bullet bullet)
                DespawnBullet(bullet);
        }

        private void DespawnBullet(Bullet bullet)
        {
            bullet.Physics.StopMove();

            _bulletPool.PushObject(bullet);
        }

        private Vector2 GetPositionFire()
        {
            return _shipPhysics.PhysicsTarget.Position + _shipPhysics.PhysicsTarget.DirectionBody * OffsetFirePosition;
        }

        private void ResetCooldownFire() => _cooldownTimer = _cooldownFire;
    }
}
