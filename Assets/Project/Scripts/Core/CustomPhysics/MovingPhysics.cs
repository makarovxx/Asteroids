using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public abstract class MovingPhysics : PhysicBase, IMovingPhysics
    {
        protected const float MaxEntitySpeed = 12f;

        protected readonly RotationResolver RotationResolver;

        public Vector2 Velocity { get; protected set; }
        public Vector2 DirectionBodyDefault => Body.right;
        
        protected MovingPhysics(Transform body, RotationResolver rotationResolver) : base(body)
        {
            RotationResolver = rotationResolver;
        }

        public override void Tick(float deltaTime) => Move(deltaTime);

        public virtual void Move(float deltaTime) => Body.Translate(Velocity * deltaTime, Space.World);

        public void SetVelocity(Vector2 velocity) => Velocity = Vector2.ClampMagnitude(velocity, MaxEntitySpeed);

        public void StopMove() => Velocity = Vector2.zero;

        public void SetRotation(float angle)
        {
            float normalizedAngle = Mathf.DeltaAngle(0f, angle);
            Body.rotation = Quaternion.Euler(0, 0, normalizedAngle);
        }
    }
}
