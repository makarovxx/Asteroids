using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Configs;
using Project.Scripts.Entities.Enemies.Asteroids;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.EnemyLifeCycle
{
    public class AsteroidLifeCycleController : IInitializable, IDisposable, ITickable
    {
        private readonly AsteroidsConfig _config;

        private readonly IPool<LargeAsteroid> _poolLarge;
        private readonly IPool<MediumAsteroid> _poolMedium;
        private readonly IPool<SmallAsteroid> _poolSmall;

        private readonly Camera _camera;

        private CancellationTokenSource _cts;

        private const int SplitCount = 3;
        private SignalBus _signalBus;

        [Inject]
        public AsteroidLifeCycleController(
            IPool<LargeAsteroid> poolLarge,
            IPool<MediumAsteroid> poolMedium,
            IPool<SmallAsteroid> poolSmall,
            AsteroidsConfig config,
            Camera camera, SignalBus signalBus)
        {
            _poolLarge = poolLarge;
            _poolMedium = poolMedium;
            _poolSmall = poolSmall;
            _config = config;
            _camera = camera;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<EnemyHitByWeaponSignal>(DespawnAsteroid);
            _cts = new CancellationTokenSource();
            RunSpawnLoop(_cts.Token).Forget();
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<EnemyHitByWeaponSignal>(DespawnAsteroid);
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        void ITickable.Tick()
        {
            // TryDespawnLarge();
            // TryDespawnMedium();
            // TryDespawnSmall();
        }

        private void DespawnAsteroid(EnemyHitByWeaponSignal signal)
        {
            
            switch (signal.EnemyDestroyed)
            {
                case LargeAsteroid largeAsteroid:
                {
                    DespawnLarge(largeAsteroid);
                    break;
                }
                case MediumAsteroid mediumAsteroid:
                {
                    DespawnMedium(mediumAsteroid);
                    break;
                }
                case SmallAsteroid smallAsteroid:
                {
                    DespawnSmall(smallAsteroid);
                    break;
                }
            }
        }

        private void TryDespawnLarge()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_poolLarge.TryGetActiveObject(out var largeAsteroid))
                    return;

                DespawnLarge(largeAsteroid);
            }
        }

        private void TryDespawnMedium()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!_poolMedium.TryGetActiveObject(out var mediumAsteroid))
                {
                    return;
                }

                DespawnMedium(mediumAsteroid);
                Debug.Log("Despawned Active Large Asteroid");
            }
        }

        private void TryDespawnSmall()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (!_poolSmall.TryGetActiveObject(out var smallAsteroid))
                {
                    Debug.Log("Не найден Active Small Asteroid");
                    return;
                }

                DespawnSmall(smallAsteroid);
                Debug.Log("Despawned Active Large Asteroid");
            }
        }

        private async UniTask RunSpawnLoop(CancellationToken ct)
        {
            while (true)
            {
                bool cancelled = await UniTask.Delay(TimeSpan.FromSeconds(_config.SpawnInterval), cancellationToken: ct)
                    .SuppressCancellationThrow();

                if (cancelled)
                    return;

                SpawnAsteroid(_poolLarge, GetRandomSpawnPosition());
            }
        }

        private void SpawnAsteroid<TAsteroid>(IPool<TAsteroid> pool, Vector2 position) where TAsteroid : Asteroid
        {
            if (!pool.TryGetObject(out TAsteroid asteroid))
                return;


            asteroid.Physics.Position = position;
            Vector2 dir = Random.insideUnitCircle.normalized;
            asteroid.Physics.SetVelocity(dir);
        }

        private void DespawnLarge(LargeAsteroid asteroid)
        {
            Vector2 spawnOrigin = asteroid.transform.position;

            ReturnToPool(asteroid, _poolLarge);

            SpawnChildren(_poolMedium, spawnOrigin).Forget();
        }

        private void DespawnMedium(MediumAsteroid asteroid)
        {
            Vector2 spawnOrigin = asteroid.transform.position;

            ReturnToPool(asteroid, _poolMedium);

            SpawnChildren(_poolSmall, spawnOrigin).Forget();
        }

        private void DespawnSmall(SmallAsteroid asteroid)
        {
            ReturnToPool(asteroid, _poolSmall);
        }

        // ─────────────────────────────────────────────────────────
        //  Спавн дочерних осколков
        // ─────────────────────────────────────────────────────────

        /// <summary>
        /// Спауним SplitCount осколков из точки origin.
        /// Направления равномерно распределены по кругу (каждые 120°)
        /// со случайным начальным углом — осколки не летят в одну точку.
        /// </summary>
        private async UniTask SpawnChildren<TChild>(IPool<TChild> childPool, Vector2 origin) where TChild : Asteroid
        {
            await UniTask.DelayFrame(3);
            Vector2[] directions = GetSpreadDirections(SplitCount);

            foreach (Vector2 _ in directions)
            {
                SpawnAsteroid(childPool, origin);
            }
        }

        /// <summary>
        /// Возвращает count направлений, равномерно разнесённых по кругу.
        /// Случайный базовый угол — каждый сплит выглядит уникально.
        /// </summary>
        private Vector2[] GetSpreadDirections(int count)
        {
            var directions = new Vector2[count];
            float baseAngle = Random.Range(0f, 360f);
            float step = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float rad = (baseAngle + step * i) * Mathf.Deg2Rad;
                directions[i] = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            }

            return directions;
        }

        // ─────────────────────────────────────────────────────────
        //  Утилиты
        // ─────────────────────────────────────────────────────────

        private void ReturnToPool<TAsteroid>(TAsteroid asteroid, IPool<TAsteroid> pool) where TAsteroid : Asteroid
        {
            asteroid.Physics.StopMove();

            pool.PushObject(asteroid);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 viewportPoint = _config.SpawnPoints[Random.Range(0, _config.SpawnPoints.Count)];
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}