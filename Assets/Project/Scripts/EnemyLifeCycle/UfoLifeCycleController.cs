using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Configs;
using Project.Scripts.Entities.Enemies.Ufo;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.EnemyLifeCycle
{
    public class UfoLifeCycleController : IInitializable, IDisposable
    {
        private readonly IPool<Ufo> _pool;
        private readonly UfoConfig  _config;
        private readonly Camera     _camera;

        private CancellationTokenSource _cts;

        [Inject]
        public UfoLifeCycleController(
            IPool<Ufo> pool,
            UfoConfig  config,
            Camera     camera)
        {
            _pool   = pool;
            _config = config;
            _camera = camera;
        }

        // ─────────────────────────────────────────────────────────
        //  IInitializable / IDisposable
        // ─────────────────────────────────────────────────────────

        public void Initialize()
        {
            _cts = new CancellationTokenSource();
            RunSpawnLoop(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        // ─────────────────────────────────────────────────────────
        //  UniTask: бесконечный спавн
        // ─────────────────────────────────────────────────────────

        private async UniTaskVoid RunSpawnLoop(CancellationToken ct)
        {
            while (true)
            {
                bool cancelled = await UniTask
                    .Delay(TimeSpan.FromSeconds(_config.SpawnInterval), cancellationToken: ct)
                    .SuppressCancellationThrow();

                if (cancelled)
                    return;

                SpawnUfo();
            }
        }

        // ─────────────────────────────────────────────────────────
        //  Спавн / Деспавн
        // ─────────────────────────────────────────────────────────

        private void SpawnUfo()
        {
            if (!_pool.TryGetObject(out Ufo ufo))
                return;

            ufo.transform.position = GetRandomSpawnPosition();
        }

        /// <summary>
        /// Деспаунит UFO — вызывается системой столкновений или оружием.
        /// Метод публичный: CollisionSystem / ProjectileHitSystem дёргают его
        /// через AsteroidLifeCycleController или напрямую.
        /// </summary>
        public void Despawn(Ufo ufo)
        {
            _pool.PushObject(ufo);
        }

        // ─────────────────────────────────────────────────────────
        //  Утилиты
        // ─────────────────────────────────────────────────────────

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 viewportPoint = _config.SpawnPoints[Random.Range(0, _config.SpawnPoints.Count)];
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}