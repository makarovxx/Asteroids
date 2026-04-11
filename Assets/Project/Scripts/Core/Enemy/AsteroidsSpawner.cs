using System;
using System.Collections.Generic;
using Project.Scripts.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Core.Enemy
{
    public class AsteroidsSpawner : MonoBehaviour
    {
        [SerializeField] private List<Vector2> _spawnPoints;
        [SerializeField] private Asteroid _prefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Count);
            Vector2 point = _spawnPoints[randomIndex];
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(point);

            Instantiate(_prefab, spawnPosition, Quaternion.identity);
        }
    }
}