using System;

namespace Project.Scripts.Infrastructure.Configs.SerializableData
{
    [Serializable]
    public class UfoData
    {
        public int PoolSize;
        public int SpawnInterval;
        public float Speed;
        public float DirectionUpdateInterval;
        public int MaxActiveUfos;
    }
}