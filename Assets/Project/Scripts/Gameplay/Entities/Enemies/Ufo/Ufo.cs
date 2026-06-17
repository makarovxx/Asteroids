using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Gameplay.Entities.Enemies.Ufo
{
    public class Ufo : Enemy<FollowPhysics>
    {
        public override ParticleSystem CollisionShipParticles { get; protected set; }
        public override EntityType EntityType => EntityType.Ufo;
        public override CollisionHandlePriority CollisionPriority => CollisionHandlePriority.Low;

        public override void Init(IPhysics physics)
        {
            base.Init(physics);
            CollisionShipParticles = GetComponent<ParticleSystem>();
        }
    }
}

