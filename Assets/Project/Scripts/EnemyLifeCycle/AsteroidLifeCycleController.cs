using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Configs;
using Project.Scripts.Entities.Enemies.Asteroids;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.EnemyLifeCycle
{
    public class AsteroidLifeCycleController : IInitializable, IDisposable
    {
        private readonly AsteroidsConfig _config;

        private readonly IPool<LargeAsteroid> _poolLarge;
        private readonly IPool<MediumAsteroid> _poolMedium;
        private readonly IPool<SmallAsteroid> _poolSmall;

        private readonly Camera _camera;

        private CancellationTokenSource _cts;

        private const int SplitCount = 3;

        [Inject]
        public AsteroidLifeCycleController(
            IPool<LargeAsteroid> poolLarge,
            IPool<MediumAsteroid> poolMedium,
            IPool<SmallAsteroid> poolSmall,
            AsteroidsConfig config,
            Camera camera)
        {
            _poolLarge = poolLarge;
            _poolMedium = poolMedium;
            _poolSmall = poolSmall;
            _config = config;
            _camera = camera;
        }

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

        private async UniTaskVoid RunSpawnLoop(CancellationToken ct)
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

                SpawnAsteroid(_poolLarge, GetRandomSpawnPosition(), _config.SpeedMin, _config.SpeedMax);
            }
        }

        private void SpawnAsteroid<TAsteroid>(
            IPool<TAsteroid> pool,
            Vector2 position,
            float speedMin,
            float speedMax,
            Vector2? direction = null)
            where TAsteroid : Asteroid
        {
            if (!pool.TryGetObject(out TAsteroid asteroid))
                return;
            
            asteroid.Physics.Position = position;
            asteroid.SetRandomRotation();

            Vector2 dir = direction ?? Random.insideUnitCircle.normalized;
            float speed = Random.Range(speedMin, speedMax);

            asteroid.Launch(dir, speed);
        }
        
        public void DespawnLarge(LargeAsteroid asteroid)
        {
            Vector2 spawnOrigin = asteroid.transform.position;

            ReturnToPool(asteroid, _poolLarge);

            SpawnChildren(_poolMedium, spawnOrigin, _config.SpeedMin, _config.SpeedMax);
        }

        /// <summary>
        /// Деспаунит MediumAsteroid и спауним из его позиции 3 Small.
        /// </summary>
        public void DespawnMedium(MediumAsteroid asteroid)
        {
            Vector2 spawnOrigin = asteroid.transform.position;

            ReturnToPool(asteroid, _poolMedium);

            SpawnChildren(_poolSmall, spawnOrigin, _config.SpeedMin, _config.SpeedMax);
        }

        /// <summary>
        /// Деспаунит SmallAsteroid — сплит не нужен.
        /// </summary>
        public void DespawnSmall(SmallAsteroid asteroid)
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
        private void SpawnChildren<TChild>(
            IPool<TChild> childPool,
            Vector2 origin,
            float speedMin,
            float speedMax)
            where TChild : Asteroid
        {
            Vector2[] directions = GetSpreadDirections(SplitCount);

            foreach (Vector2 dir in directions)
            {
                SpawnAsteroid(childPool, origin, speedMin, speedMax, dir);
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

        private void ReturnToPool<TAsteroid>(TAsteroid asteroid, IPool<TAsteroid> pool)
            where TAsteroid : Asteroid
        {
            asteroid.Stop();
            pool.PushObject(asteroid);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 viewportPoint = _config.SpawnPoints[Random.Range(0, _config.SpawnPoints.Count)];
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}