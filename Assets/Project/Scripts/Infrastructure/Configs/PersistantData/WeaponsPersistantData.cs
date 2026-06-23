using System;
using Project.Scripts.Gameplay.Entities.Projectile;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Configs.PersistantData
{
    [Serializable]
    public class WeaponsPersistantData
    {
        public Bullet BulletPrefab;
        public Transform BulletContainer;
        public Laser LaserPrefab;
    }
}