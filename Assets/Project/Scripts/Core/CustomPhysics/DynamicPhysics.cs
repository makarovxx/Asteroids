using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class DynamicPhysics : EntityPhysicsBase, IFixedTickable
    {
        private float _acceleration;
        private float _damping;
        private float _rotationSpeed;
        
        [Inject]
        public DynamicPhysics(Transform body, Vector2 velocity, RotationResolver rotationResolver, float acceleration, float damping, float rotationSpeed) : base(body, velocity, rotationResolver)
        {
            _acceleration = acceleration;
            _damping = damping;
            _rotationSpeed = rotationSpeed;
        }

        void IFixedTickable.FixedTick()
        {
            
        }
        
        public void Accelerate(float deltaTime)
        {
            Velocity += DirectionBodyDefault * _acceleration * deltaTime;
            Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        }
        
        public void ApplyDamping(float deltaTime)
        {
            Velocity = Vector2.Lerp(Velocity, Vector2.zero, _damping * deltaTime);
        }
        
        public void RotateSmoothly(float deltaTime, DirectionRotation direction)
        {
            if (direction == DirectionRotation.None)
                return;

            float targetAngle = RotationResolver.GetAngle(direction);

            float newAngle = Mathf.MoveTowardsAngle(
                Body.eulerAngles.z,
                targetAngle,
                _rotationSpeed * deltaTime
            );

            Body.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }
    }
}