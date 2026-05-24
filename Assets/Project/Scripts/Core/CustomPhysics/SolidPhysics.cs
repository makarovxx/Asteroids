using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class SolidPhysics : EntityPhysicsBase, IFixedTickable
    {
        public SolidPhysics(Transform body, RotationResolver rotationResolver) : base(body, rotationResolver)
        {
        }

        public new void SetRotation(float angle)
        {
            Body.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetRandomRotation() => SetRotation(Random.Range(0f, 360f));

        public void Launch(Vector2 direction, float speed)
        {
            if (direction == Vector2.zero)
                direction = Random.insideUnitCircle.normalized;

            if (direction == Vector2.zero)
                direction = Vector2.right;

            SetVelocity(direction.normalized * speed);
            SetRotation(RotationResolver.GetAngle(direction));
        }

        public new void StopMove() => base.StopMove();

        public void FixedTick()
        {
            Move(Time.fixedDeltaTime);
        }
    }
}
