using System;

namespace Project.Scripts.Infrastructure.Configs.SerializableData
{
    [Serializable]
    public sealed class LaserData
    {
        public int MaxCharges;
        public float RechargeTime;
        public float ActiveTime;
    }
}
