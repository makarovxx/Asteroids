using System;

namespace Project.Scripts.Infrastructure.Configs.SerializableData
{
    [Serializable]
    public class BulletData
    {
        public int PoolSize = 30;
        public float LifeTime = 2.5f;
        public float Speed = 7f;
    }
}