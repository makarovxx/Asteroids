using System.Collections.Generic;
using Project.Scripts.Entities;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.Core.Enemy
{
    public class AsteroidsSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Vector2> _spawnPoints;

        private IPool<Asteroid> _pool;

        [Inject]
        private void Construct(IPool<Asteroid> pool)
        {
            _pool = pool;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnAsteroid();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                DespawnAsteroid();
            }
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
                Random.Range(2f, 5f);

            asteroid.Launch(direction, speed);
        }

        private void DespawnAsteroid()
        {
            // тестовый despawn

            // потом тут будет collision/lifetime
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