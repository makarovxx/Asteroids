using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Gameplay.Entities.Enemies.Asteroids;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.Gameplay.EnemyLifeCycle
{
    public class AsteroidLifeCycleController : IInitializable, IDisposable
    {
        private readonly AsteroidsData _asteroidsData;
        private readonly IPool<LargeAsteroid> _poolLarge;
        private readonly IPool<MediumAsteroid> _poolMedium;
        private readonly IPool<SmallAsteroid> _poolSmall;
        private readonly SignalBus _signalBus;
        private readonly Camera _camera;
        
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
        public AsteroidLifeCycleController(
            IPool<LargeAsteroid> poolLarge,
            IPool<MediumAsteroid> poolMedium,
            IPool<SmallAsteroid> poolSmall,
            AsteroidsData asteroidsData,
            Camera camera, SignalBus signalBus)
        {
            _poolLarge = poolLarge;
            _poolMedium = poolMedium;
            _poolSmall = poolSmall;
            _asteroidsData = asteroidsData;
            _camera = camera;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<EnemyHitByWeaponSignal>(DespawnAsteroid);
            _signalBus.Subscribe<ShipHitEnemy>(HandleShipHit);
            _cts = new CancellationTokenSource();
            RunSpawnLoop(_cts.Token).Forget();
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<EnemyHitByWeaponSignal>(DespawnAsteroid);
            _signalBus.Unsubscribe<ShipHitEnemy>(HandleShipHit);
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
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

        private async UniTask RunSpawnLoop(CancellationToken ct)
        {
            while (true)
            {
                bool cancelled = await UniTask.Delay(TimeSpan.FromSeconds(_asteroidsData.SpawnInterval), cancellationToken: ct)
                    .SuppressCancellationThrow();

                if (cancelled)
                    return;

                SpawnAsteroid(_poolLarge, GetRandomSpawnPosition());
            }
        }

        private void SpawnAsteroid<TAsteroid>(IPool<TAsteroid> pool, Vector2 position, Vector2 direction = default)
            where TAsteroid : Asteroid
        {
            if (!pool.TryGetObject(out TAsteroid asteroid))
                return;

            asteroid.Physics.Position = position;
            if (direction == default)
                direction = Random.insideUnitCircle.normalized;
            
            var speed = Random.Range(_asteroidsData.SpeedMin, _asteroidsData.SpeedMax);
            asteroid.Physics.SetVelocity(direction * speed);
        }

        private void DespawnLarge(LargeAsteroid asteroid)
        {
            Vector2 spawnOrigin = asteroid.transform.position;

            ReturnToPool(asteroid, _poolLarge);

            SpawnChildren(_poolMedium, spawnOrigin);
        }

        private void DespawnMedium(MediumAsteroid asteroid)
        {
            Vector2 spawnOrigin = asteroid.transform.position;

            ReturnToPool(asteroid, _poolMedium);

            SpawnChildren(_poolSmall, spawnOrigin);
        }

        private void DespawnSmall(SmallAsteroid asteroid)
        {
            ReturnToPool(asteroid, _poolSmall);
        }
        
        private void HandleShipHit(ShipHitEnemy signal)
        {
            if (signal.HitBy is Asteroid asteroid)
            {
                Debug.Log("Asteroid hit Ship");
            }
        }
        
        private void SpawnChildren<TChild>(IPool<TChild> childPool, Vector2 origin) where TChild : Asteroid
        {
            Vector2[] directions = GetSpreadDirections(_asteroidsData.AmountSpawnAfterDestroy);

            foreach (Vector2 dir in directions)
            {
                SpawnAsteroid(childPool, origin, dir);
            }
        }
        
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

        private void ReturnToPool<TAsteroid>(TAsteroid asteroid, IPool<TAsteroid> pool) where TAsteroid : Asteroid
        {
            asteroid.Physics.StopMove();

            pool.PushObject(asteroid);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 viewportPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}