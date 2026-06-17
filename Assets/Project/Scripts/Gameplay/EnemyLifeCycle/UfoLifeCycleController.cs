using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Gameplay.Entities.Enemies.Ufo;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.Gameplay.EnemyLifeCycle
{
    public class UfoLifeCycleController : IInitializable, IDisposable
    {
        private readonly UfoData _config;
        private readonly IPool<Ufo> _pool;
        private readonly Camera _camera;
        private readonly SignalBus _signalBus;

        private readonly Vector2[] _spawnPoints = {
            new Vector2(0.1f, 0.25f),
            new Vector2(0.1f, 0.5f),
            new Vector2(0.1f, 0.75f),
            new Vector2(0.9f, 0.25f),
            new Vector2(0.9f, 0.5f),
            new Vector2(0.9f, 0.75f),
        };

        private CancellationTokenSource _cts;

        [Inject]
        public UfoLifeCycleController(IPool<Ufo> pool, UfoData config, Camera camera, SignalBus signalBus)
        {
            _pool = pool;
            _config = config;
            _camera = camera;
            _signalBus = signalBus;
        }


        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<EnemyHitByWeaponSignal>(DespawnUfo);
            _signalBus.Subscribe<ShipHitEnemy>(HandleShipHit);
            _cts = new CancellationTokenSource();
            RunSpawnLoop(_cts.Token).Forget();
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<EnemyHitByWeaponSignal>(DespawnUfo);
            _signalBus.Unsubscribe<ShipHitEnemy>(HandleShipHit);
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTask RunSpawnLoop(CancellationToken ct)
        {
            while (true)
            {
                bool cancelled = await UniTask
                    .Delay(
                        TimeSpan.FromSeconds(_config.SpawnInterval),
                        cancellationToken: ct)
                    .SuppressCancellationThrow();

                if (cancelled)
                    return;

                SpawnUfo();
            }
        }

        private void SpawnUfo()
        {
            if (!_pool.TryGetObject(out Ufo ufo))
                return;

            Vector2 spawnPosition = GetRandomSpawnPosition();
            ufo.Physics.Position = spawnPosition;
        }

        private void DespawnUfo(EnemyHitByWeaponSignal enemyHitByWeaponSignal)
        {
            if (enemyHitByWeaponSignal.EnemyDestroyed is Ufo ufo)
            {
                ufo.Physics.StopMove();
                _pool.PushObject(ufo);
            }
        }

        private void HandleShipHit(ShipHitEnemy signal)
        {
            if (signal.HitBy is Ufo ufo)
            {
                ufo.Physics.Reset();
            }
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 viewportPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}