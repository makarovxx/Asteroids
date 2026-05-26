using System;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class PlayerConfig
    {
        public Transform Body;
        public float Acceleration;
        public float Damping;
        public float RotationSpeed;
    }
}