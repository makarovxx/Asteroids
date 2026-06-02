using System;
using System.Collections.Generic;
using Project.Scripts.Entities.Enemies.Ufo;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class UfoConfig
    {
        public Ufo Prefab;
        public int SpawnInterval = 10;
        public int Speed;
        public Transform Container;
        public int PoolSize;
        public List<Vector2> SpawnPoints;
    }
}