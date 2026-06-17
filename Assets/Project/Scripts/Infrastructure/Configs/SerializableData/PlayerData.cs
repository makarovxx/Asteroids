using System;

namespace Project.Scripts.Infrastructure.Configs.SerializableData
{
    [Serializable]
    public class PlayerData
    {
        public int AmountHealth;
        public float Acceleration;
        public float Damping;
        public float RotationSpeed;
    }
}