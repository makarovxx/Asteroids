using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public sealed class ShipPhysics : SolidPhysics
    {
        private readonly float _acceleration;
        private readonly float _damping;
        private readonly float _rotationSpeed;
        private bool _controlEnabled = true;
        
        private readonly InputManager _input;
        private readonly RotationResolver _rotationResolver;

        [Inject]
        public ShipPhysics(Transform body, float acceleration, float damping,
            float rotationSpeed, InputManager input, RotationResolver rotationResolver) : base(body)
        {
            _acceleration = acceleration;
            _damping = damping;
            _rotationSpeed = rotationSpeed;
            _input = input;
            _rotationResolver = rotationResolver;
        }

        public override void Tick(float deltaTime)
        {
            HandleMovement(deltaTime);
            HandleRotation(deltaTime);
        }

        public void SetControlability(bool enabled)
        {
            _controlEnabled = enabled;
        }

        private void Accelerate(float deltaTime)
        {
            Velocity += DirectionBody * (_acceleration * deltaTime);
            Velocity = Vector2.ClampMagnitude(Velocity, MaxEntitySpeed);
        }

        private void ApplyDamping(float deltaTime)
        {
            Velocity = Vector2.Lerp(Velocity, Vector2.zero, _damping * deltaTime);
        }

        private void HandleRotation(float deltaTime)
        {
            if(!_controlEnabled) return;
            Vector2 direction = _input.GetRotationDirection();

            if (direction == Vector2.zero)
                return;

            float targetAngle = _rotationResolver.GetAngle(direction);

            float newAngle = Mathf.MoveTowardsAngle(
                Body.eulerAngles.z,
                targetAngle,
                _rotationSpeed * deltaTime
            );

            Body.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }

        private void HandleMovement(float deltaTime)
        {
            if (_input.IsAccelerateInput() && _controlEnabled)
                Accelerate(deltaTime);
            else
                ApplyDamping(deltaTime);

            Move(deltaTime);
        }
    }
}