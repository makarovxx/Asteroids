using System;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Plugins;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Entities
{
    public class Asteroid : MonoBehaviour, ICreatable
    {
        private SolidPhysics _physics;

        public void Initialize(SolidPhysics physics)
        {
            _physics = physics;
        }

        public void Launch(Vector2 direction, float speed)
        {
            _physics.Launch(direction, speed);
        }

        public void Stop()
        {
            _physics.Stop();
        }

        public void SetRandomRotation()
        {
            _physics.SetRandomRotation();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 direction =
                Random.insideUnitCircle.normalized;
            _physics.SetVelocity(direction);
        }
    }
}