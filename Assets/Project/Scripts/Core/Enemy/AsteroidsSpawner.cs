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
        [SerializeField] private List<Vector2> _spawnPoints;
        [SerializeField] private float _asteroidSpeed = 3f;

        private IPool<Asteroid> _asteroidsPool;

        [Inject]
        private void Construct(IPool<Asteroid> asteroidsPool)
        {
            _asteroidsPool = asteroidsPool;
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
            if (!_asteroidsPool.TryGetObject(out Asteroid asteroid))
                return;

            int randomIndex = Random.Range(0, _spawnPoints.Count);
            Vector2 point = _spawnPoints[randomIndex];
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(point);
            Vector2 direction = Random.insideUnitCircle.normalized;

            asteroid.Spawn(spawnPosition, direction, _asteroidSpeed);
        }

        private void DespawnAsteroid()
        {
            if (!_asteroidsPool.TryGetActiveObject(out Asteroid asteroid))
                return;

            asteroid.StopMove();
            _asteroidsPool.PushObject(asteroid);
        }
    }
}
