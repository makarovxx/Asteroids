using System;

namespace Project.Scripts.Infrastructure.Configs.SerializableData
{
    [Serializable]
    public class AsteroidsData
    {
        public float SpawnInterval;
        public int AmountSpawnAfterDestroy;
        public int PoolSizeLarge;
        public int PoolSizeMedium;
        public int PoolSizeSmall;
        public float SpeedMin;
        public float SpeedMax;
        public int MaxActiveAsteroids;
    }
}