using System;
using System.Collections.Generic;
using Project.Scripts.Entities;
using Project.Scripts.Entities.Enemies.Asteroids;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class AsteroidsConfig
    {
        public LargeAsteroid PrefabLargeAsteroid;
        public MediumAsteroid PrefabMediumAsteroid;
        public SmallAsteroid PrefabSmallAsteroid;
        public Transform AsteroidsContainer;
        public float SpawnInterval = 10;
        public int CountSpawnAsteroidAfterCrash = 3;
        [FormerlySerializedAs("SpawnLargeAsteroidsPoints")] public List<Vector2> SpawnPoints;
        public int PoolSizeLarge;
        public int PoolSizeMedium;
        public int PoolSizeSmall;
        public float SpeedMin;
        public float SpeedMax;
    }
}