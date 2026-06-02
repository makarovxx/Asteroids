using System;
using Project.Scripts.Entities.Ship;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class PlayerConfig
    {
        public Ship Prefab;
        public Vector2 SpawnPoint;
        public Transform Container;
        public float Acceleration;
        public float Damping;
        public float RotationSpeed;
    }
}
