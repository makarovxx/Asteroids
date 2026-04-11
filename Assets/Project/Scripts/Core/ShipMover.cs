using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Core.InputManageSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Core
{
    public class ShipMover : MonoBehaviour
    {
        [SerializeField] private float acceleration = 8f;
        [SerializeField] private float maxSpeed = 12f;
        [SerializeField] private float damping = 0.8f;
        [SerializeField] private float rotationSpeed = 720f;
        
        private RotationResolver _rotationResolver;
        private Vector2 DirectionShipDefault => transform.right;
        private DesktopInput _input;
        private Vector2 _velocity;

        private void Awake()
        {
            _input = new DesktopInput();
            _rotationResolver = new RotationResolver();
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
    }
}