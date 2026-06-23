using System;
using Project.Scripts.Core.TickableSystem;
using Project.Scripts.Gameplay.Entities.Enemies.Asteroids;
using Project.Scripts.Gameplay.Utilities.World;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Plugins;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.Gameplay.EnemyLifeCycle
{
    public sealed class SpawnPointsProvider
    {
        public Vector2[] SpawnPoints { get; } =
        {
            new(0.1f, 0.25f),
            new(0.1f, 0.5f),
            new(0.1f, 0.75f),
            new(0.9f, 0.25f),
            new(0.9f, 0.5f),
            new(0.9f, 0.75f),
        };
    }

    public class AsteroidLifeCycleController : IInitializable, IDisposable, IBehaviourTickable
    {
        [Inject] private readonly SignalBus _signalBus;

        private readonly AsteroidsData _asteroidsData;
        private readonly IPool<LargeAsteroid> _poolLarge;
        private readonly IPool<MediumAsteroid> _poolMedium;
        private readonly IPool<SmallAsteroid> _poolSmall;

        private readonly SpawnPointsProvider _spawnPointsProvider;
        private readonly CameraSpaceMapper _cameraMapper;

        private float _spawnTimer;

        [Inject]
        public AsteroidLifeCycleController(IPool<LargeAsteroid> poolLarge, IPool<MediumAsteroid> poolMedium,
            IPool<SmallAsteroid> poolSmall, AsteroidsData asteroidsData, SpawnPointsProvider spawnPointsProvider,
            CameraSpaceMapper cameraMapper)
        {
            _poolLarge = poolLarge;
            _poolMedium = poolMedium;
            _poolSmall = poolSmall;
            _asteroidsData = asteroidsData;
            _spawnPointsProvider = spawnPointsProvider;
            _cameraMapper = cameraMapper;
        }

        void IInitializable.Initialize()
        {
            SetSpawnTimer();
            _signalBus.Subscribe<WeaponHitEnemy>(DespawnAsteroid);
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<WeaponHitEnemy>(DespawnAsteroid);
        }

        public void Tick(float deltaTime)
        {
            _spawnTimer -= deltaTime;

            if (_spawnTimer > 0f)
                return;

            SetSpawnTimer();

            SpawnAsteroid(_poolLarge, GetRandomSpawnPosition());
        }

        private void DespawnAsteroid(WeaponHitEnemy signal)
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
            Vector2 viewportPoint =
                _spawnPointsProvider.SpawnPoints[Random.Range(0, _spawnPointsProvider.SpawnPoints.Length)];
            return _cameraMapper.ViewportToWorld(viewportPoint);
        }

        private void SetSpawnTimer() => _spawnTimer = _asteroidsData.SpawnInterval;
    }
}