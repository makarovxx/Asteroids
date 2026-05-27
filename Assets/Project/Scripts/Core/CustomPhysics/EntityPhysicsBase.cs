using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class EntityPhysicsBase : IPhysicsObject
    {
        protected const float MaxEntitySpeed = 12f;
        public Vector2 Position
        {
            get => Body.position;
            set => Body.position = value;
        }
        
        protected Transform Body;
        protected RotationResolver RotationResolver;
        
        public Vector2 Velocity { get; protected set; }
        protected Vector2 DirectionBodyDefault => Body.right;

        public bool IsActive => Body.gameObject.activeSelf;

        protected EntityPhysicsBase(Transform body, RotationResolver rotationResolver)
        {
            Body = body;
            RotationResolver = rotationResolver;
        }

        public virtual void Tick(float deltaTime) => Move(deltaTime);

        protected void Move(float deltaTime) => Body.Translate(Velocity * deltaTime, Space.World);
        
        protected void SetVelocity(Vector2 velocity) => Velocity = Vector2.ClampMagnitude(velocity, MaxEntitySpeed);

        protected void StopMove() => Velocity = Vector2.zero;

        protected void SetRotation(float angle)
        {
            float normalizedAngle = Mathf.DeltaAngle(0f, angle);
            Body.rotation = Quaternion.Euler(0, 0, normalizedAngle);
        }
    }
}