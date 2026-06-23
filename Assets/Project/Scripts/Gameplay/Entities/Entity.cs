using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Gameplay.Entities
{
    public abstract class Entity : MonoBehaviour, ICollidable
    {
        public abstract EntityType EntityType { get; }
        public abstract CollisionHandlePriority CollisionPriority { get; }
        public IPhysics Physics { get; private set; }

        public virtual void Init(IPhysics physics)
        {
            Physics = physics;
        }
    }
    public abstract class Entity<TPhysics> : Entity where TPhysics : class, IMovingPhysics
    {
        public new TPhysics Physics => (TPhysics)base.Physics;
    }
}
