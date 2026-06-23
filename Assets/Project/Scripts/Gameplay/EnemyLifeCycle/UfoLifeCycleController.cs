using System;
using Project.Scripts.Core.TickableSystem;
using Project.Scripts.Gameplay.Entities.Enemies.Ufo;
using Project.Scripts.Gameplay.Utilities.World;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.Gameplay.EnemyLifeCycle
{
    public class UfoLifeCycleController : IInitializable, IDisposable, IBehaviourTickable
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private readonly UfoData _ufoData;
        private readonly IPool<Ufo> _pool;
        
        private readonly CameraSpaceMapper _cameraMapper;
        private readonly SpawnPointsProvider _spawnPointsProvider;
        private float _spawnTimer;

        [Inject]
        public UfoLifeCycleController(IPool<Ufo> pool, UfoData ufoData, SpawnPointsProvider spawnPointsProvider, CameraSpaceMapper cameraMapper)
        {
            _pool = pool;
            _ufoData = ufoData;
            _spawnPointsProvider = spawnPointsProvider;
            _cameraMapper = cameraMapper;
        }


        void IInitializable.Initialize()
        {
            SetSpawnTimer();
            _signalBus.Subscribe<WeaponHitEnemy>(DespawnUfo);
            _signalBus.Subscribe<ShipCollisionEnemy>(HandleShipHit);
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<WeaponHitEnemy>(DespawnUfo);
            _signalBus.Unsubscribe<ShipCollisionEnemy>(HandleShipHit);
        }

        public void Tick(float deltaTime)
        {
            _spawnTimer -= deltaTime;

            if (_spawnTimer > 0f)
                return;
            
            SetSpawnTimer();
            SpawnUfo();
        }

        private void SpawnUfo()
        {
            if (!_pool.TryGetObject(out Ufo ufo))
                return;

            Vector2 spawnPosition = GetRandomSpawnPosition();
            ufo.Physics.Position = spawnPosition;
            ufo.Physics.TrySetTarget();
        }

        private void DespawnUfo(WeaponHitEnemy weaponHitEnemy)
        {
            if (weaponHitEnemy.EnemyDestroyed is Ufo ufo)
            {
                ufo.Physics.StopMove();
                _pool.PushObject(ufo);
            }
        }

        private void HandleShipHit(ShipCollisionEnemy signal)
        {
            if (signal.HitBy is Ufo ufo)
                ufo.Physics.Reset();
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 viewportPoint = _spawnPointsProvider.SpawnPoints[Random.Range(0, _spawnPointsProvider.SpawnPoints.Length)];
            return _cameraMapper.ViewportToWorld(viewportPoint);
        }

        private void SetSpawnTimer() => _spawnTimer = _ufoData.SpawnInterval;
    }
}