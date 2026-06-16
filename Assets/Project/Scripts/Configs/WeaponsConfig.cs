using System;
using Project.Scripts.Entities.Projectile;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class WeaponsConfig
    {
        public Bullet BulletPrefab;
        public Transform FirePoint;
        public Transform BulletContainer;
        public int PoolSizeBullets = 30;
        public float BulletLifeTime = 2.5f;
        public float BulletSpeed = 7f;
    }
}