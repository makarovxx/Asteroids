using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class ShipPhysics : EntityPhysicsBase, IFixedTickable
    {
        private float _acceleration;
        private float _damping;
        private float _rotationSpeed;
        private DesktopInput _input;
        
        [Inject]
        public ShipPhysics(Transform body, Vector2 velocity, RotationResolver rotationResolver, float acceleration, float damping, float rotationSpeed, DesktopInput input) : base(body, velocity, rotationResolver)
        {
            _acceleration = acceleration;
            _damping = damping;
            _rotationSpeed = rotationSpeed;
            _input = input;
        }

        public void FixedTick()
        {
            HandleMovement(Time.fixedDeltaTime);
            HandleRotation(Time.fixedDeltaTime);
        }

        private void Accelerate(float deltaTime)
        {
            Velocity += DirectionBodyDefault * _acceleration * deltaTime;
            Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        }

        private void ApplyDamping(float deltaTime)
        {
            Velocity = Vector2.Lerp(Velocity, Vector2.zero, _damping * deltaTime);
        }

        private void HandleRotation(float deltaTime)
        {
            DirectionRotation direction = _input.GetRotationDirection();
            
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
        
        private void HandleMovement(float deltaTime)
        {
            if (_input.IsThrusting())
                Accelerate(deltaTime);
            else
                ApplyDamping(deltaTime);

            Move(deltaTime);
        }
    }
}