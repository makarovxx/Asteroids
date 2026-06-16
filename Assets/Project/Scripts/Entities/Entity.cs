using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public abstract EntityType EntityType { get; }

        public IPhysics Physics { get; private set; }

        public void Init(IPhysics physics)
        {
            Physics = physics;

        }
    }

    public abstract class Entity<TPhysics> : Entity
        where TPhysics : class, IPhysics
    {
        public new TPhysics Physics => (TPhysics)base.Physics;
    }
}
