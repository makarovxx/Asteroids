using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class SolidPhysics : EntityMovement
    {
        private readonly RotationResolver _rotationResolver;
        
        public SolidPhysics(Transform body, Vector2 startVelocity, RotationResolver rotationResolver) : base(body, startVelocity)
        {
            _rotationResolver = rotationResolver;
        }

        public void SetRotation(float angle)
        {
            Body.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetRandomRotation() => Body.rotation = Random.rotation;
    }
}