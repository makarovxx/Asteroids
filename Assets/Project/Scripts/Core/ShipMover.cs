using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core
{
    public class ShipMover : MonoBehaviour
    {
        [SerializeField] private float acceleration = 8f;
        [SerializeField] private float maxSpeed = 12f;
        [SerializeField] private float damping = 0.8f;
        [SerializeField] private float rotationSpeed = 720f;
        [SerializeField] private float angle;
        private RotationResolver _rotationResolver;
        private DesktopInput _input;
        private Vector2 DirectionShipDefault => transform.right;
        private Vector2 _velocity;

        [Inject]
        private void Construct(RotationResolver rotationResolver, DesktopInput input)
        {
            _rotationResolver = rotationResolver;
            _input = input;
        }
        
        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            if (_input.IsThrusting())
                Accelerate();
            else
                ApplyDamping();

            Move();
        }

        private void HandleRotation()
        {
            DirectionRotation direction = _input.GetRotationDirection();

            if (direction == DirectionRotation.None)
                return;

            float targetAngle = _rotationResolver.GetAngle(direction);

            float newAngle = Mathf.MoveTowardsAngle(
                transform.eulerAngles.z,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }

        private void Accelerate()
        {
            _velocity += DirectionShipDefault * acceleration * Time.deltaTime;
            _velocity = Vector2.ClampMagnitude(_velocity, maxSpeed);
        }

        private void ApplyDamping()
        {
            _velocity = Vector2.Lerp(_velocity, Vector2.zero, damping * Time.deltaTime);
        }

        private void Move()
        {
            transform.Translate(_velocity * Time.deltaTime, Space.World);
        }
        [ContextMenu("ASd")]
        protected void SetRotation()
        {
            float normalizedAngle = Mathf.DeltaAngle(0f, angle);
            transform.rotation = Quaternion.Euler(0, 0, normalizedAngle);
        }
    }
}