using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class EntityPhysicsBase : IPhysicsObject
    {
        public Vector2 Velocity { get; protected set; }
        public Vector2 DirectionBodyDefault => Body.right;

        public bool IsActive => Body.gameObject.activeSelf;

        protected Transform Body;
        protected float MaxSpeed = 12f;
        
        protected RotationResolver RotationResolver;

        public EntityPhysicsBase(Transform body, RotationResolver rotationResolver)
        {
            Body = body;
            RotationResolver = rotationResolver;
        }

        public virtual void Tick(float deltaTime) => Move(deltaTime);

        protected void Move(float deltaTime) => Body.Translate(Velocity * deltaTime, Space.World);

        protected Vector2 GetPosition() => Body.position;
        
        protected void SetPosition(Vector2 position) => Body.position = position;
        
        protected void SetVelocity(Vector2 velocity) => Velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);

        protected void StopMove() => Velocity = Vector2.zero;

        protected void SetRotation(float angle)
        {
            float normalizedAngle = Mathf.DeltaAngle(0f, angle);
            Body.rotation = Quaternion.Euler(0, 0, normalizedAngle);
        }
    }
}