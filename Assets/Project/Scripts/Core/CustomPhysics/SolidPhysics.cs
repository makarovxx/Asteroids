using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class SolidPhysics : EntityPhysicsBase, IFixedTickable
    {
        public SolidPhysics(Transform body, RotationResolver rotationResolver) : base(body, rotationResolver)
        {
        }

        public void SetRotation(float angle)
        {
            Body.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetRandomRotation() => Body.rotation = Random.rotation;
        public void FixedTick()
        {
            Move(Time.fixedDeltaTime);
        }
    }
}