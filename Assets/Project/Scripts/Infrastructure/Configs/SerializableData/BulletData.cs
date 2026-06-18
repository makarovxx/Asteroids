using System;

namespace Project.Scripts.Infrastructure.Configs.SerializableData
{
    [Serializable]
    public class BulletData
    {
        public int PoolSize;
        public float LifeTime;
        public float Speed;
        public float CooldownFire;
    }
}