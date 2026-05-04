using System;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class PlayerConfig
    {
        public Transform Body;
        public Vector2 Velocity;
        public float Acceleration;
        public float Damping;
        public float RotationSpeed;
    }
}