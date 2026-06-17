using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Infrastructure.Configs.SerializableData
{
    [Serializable]
    public class UfoData
    {
        public int PoolSize;
        public int SpawnInterval;
        public float Speed;
        public int MaxActiveUfos;
    }
}