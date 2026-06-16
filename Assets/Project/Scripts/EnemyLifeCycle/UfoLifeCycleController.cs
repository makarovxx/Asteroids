using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Configs;
using Project.Scripts.Entities.Enemies.Ufo;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.EnemyLifeCycle
{
    public class UfoLifeCycleController : IInitializable, IDisposable, ITickable
    {
        private readonly UfoConfig _config;
        private readonly IPool<Ufo> _pool;
        private readonly Camera _camera;
        private readonly SignalBus _signalBus;

        private CancellationTokenSource _cts;

        [Inject]
        public UfoLifeCycleController(IPool<Ufo> pool, UfoConfig config, Camera camera, SignalBus signalBus)
        {
            _pool = pool;
            _config = config;
            _camera = camera;
            _signalBus = signalBus;
        }


        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<EnemyHitByWeaponSignal>(DespawnUfo);
            _cts = new CancellationTokenSource();
            RunSpawnLoop(_cts.Token).Forget();
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<EnemyHitByWeaponSignal>(DespawnUfo);
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }


        void ITickable.Tick()
        {
            // if (Input.GetKeyDown(KeyCode.Y))
            //     TryDespawnUfo();
        }

        // private void TryDespawnUfo()
        // {
        //     if (!_pool.TryGetActiveObject(out Ufo ufo))
        //     {
        //         Debug.Log("[UFO] Нет активных UFO для деспауна");
        //         return;
        //     }
        //
        //     // DespawnUfo(ufo);
        // }


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

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 viewportPoint = _config.SpawnPoints[Random.Range(0, _config.SpawnPoints.Count)];
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}