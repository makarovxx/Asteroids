using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Entities
{
    public class PhysicalEntity : MonoBehaviour
    {
        public IPhysics Physics { get; protected set; }

        public void Init(IPhysics physics)
        {
            Physics = physics;
        }
    }

    public abstract class PhysicalEntity<TPhysics> : PhysicalEntity
        where TPhysics : class, IPhysics
    {
        public new TPhysics Physics => (TPhysics)base.Physics;

        public void Init(TPhysics physics)
        {
            base.Init(physics);
            Debug.Log(this.GetType().Name);
        }
    }
}
