using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Gameplay.Entities.Enemies.Asteroids
{
    public class Asteroid : Enemy<SolidPhysics>
    {
        public override ParticleSystem CollisionShipParticles { get; protected set; }
        public override CollisionHandlePriority CollisionPriority => CollisionHandlePriority.Low;
        public override void Init(IPhysics physics)
        {
            base.Init(physics);
            CollisionShipParticles = GetComponent<ParticleSystem>();
        }
    }
}
