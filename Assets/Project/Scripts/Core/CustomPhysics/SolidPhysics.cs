using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class SolidPhysics : EntityPhysicsBase
    {
        public SolidPhysics(Transform body, RotationResolver rotationResolver) : base(body, rotationResolver)
        {
        }

        public void Launch(Vector2 direction, float speed)
        {
            SetVelocity(direction.normalized * speed);
        }

        public void Stop()
        {
            StopMove();
        }

        public void SetRandomRotation()
        {
            float angle = Random.Range(0f, 360f);

            Body.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}