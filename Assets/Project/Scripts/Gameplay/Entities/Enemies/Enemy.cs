using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Gameplay.Entities.Enemies
{
    public abstract class Enemy<TPhysics> : Entity<TPhysics> where TPhysics : class, IPhysics
    {
        public abstract ParticleSystem CollisionShipParticles { get; protected set; }
        public override EntityType EntityType { get; }
        public abstract override CollisionHandlePriority CollisionPriority { get; }
    }
}