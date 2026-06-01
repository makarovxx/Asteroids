using System.Collections.Generic;
using Project.Scripts.Entities;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.Core.Enemy
{
    public class AsteroidsSpawner
    {
        [SerializeField]
        private List<Vector2> _spawnPoints;
        [SerializeField]
        private float _minAsteroidSpeed = 2f;
        [SerializeField]
        private float _maxAsteroidSpeed = 5f;

        private IPool<Asteroid> _pool;

        [Inject]
        private void Construct(IPool<Asteroid> pool)
        {
            _pool = pool;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                SpawnAsteroid();

            if (Input.GetKeyDown(KeyCode.E))
                DespawnAsteroid();
        }

        private void SpawnAsteroid()
        {
            if (!_pool.TryGetObject(out Asteroid asteroid))
                return;

            asteroid.transform.position =
                GetRandomSpawnPosition();

            asteroid.SetRandomRotation();

            Vector2 direction =
                Random.insideUnitCircle.normalized;

            float speed =
                Random.Range(_minAsteroidSpeed, _maxAsteroidSpeed);

            asteroid.Launch(direction, speed);
        }

        private void DespawnAsteroid()
        {
            if (!_pool.TryGetActiveObject(out Asteroid asteroid))
                return;

            asteroid.Stop();
            _pool.PushObject(asteroid);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            int randomIndex =
                Random.Range(0, _spawnPoints.Count);

            Vector2 viewportPoint =
                _spawnPoints[randomIndex];

            return Camera.main
                .ViewportToWorldPoint(viewportPoint);
        }
    }
}
