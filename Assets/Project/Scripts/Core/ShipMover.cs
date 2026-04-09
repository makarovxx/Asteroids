using Project.Scripts.Core.InputManageSystem;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class ShipMover : MonoBehaviour
    {
        [SerializeField] private float acceleration = 8f;
        [SerializeField] private float maxSpeed = 12f;
        [SerializeField] private float damping = 0.8f;

        [SerializeField] private ShipRotation rotation;

        private DesktopInput _input;
        private Vector2 _velocity;
        private bool _isDynamic;
        private void Awake()
        {
            _input = new DesktopInput();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_input.IsThrusting())
                Accelerate();
            else
                ApplyDamping();

            Move();
        }

        private void Accelerate()
        {
            _velocity += rotation.Forward * acceleration * Time.deltaTime;
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
        
        [ContextMenu("ApplyVelocity")]
        private void ApplyVelocity()
        {
            _velocity = Vector2.up;
        }
    }
    
    public class CustomPhysics : IMovable
    {
        private float _maxVelocity;
        private float _dampingModifier;
        private float _accelerationModifier;
        public Vector2 CurrentVelocity { get; private set; }

        public CustomPhysics(float maxVelocity, float dampingModifier, float accelerationModifier, Vector2 startVelocity)
        {
            _maxVelocity = maxVelocity;
            _dampingModifier = dampingModifier;
            _accelerationModifier = accelerationModifier;
            CurrentVelocity = startVelocity;
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IMovable
    {
        void Move();
    }
    
    public class EntityMovement : IMovable
    {
        private Transform _transform;
        private CustomPhysics _physicComponent;

        public EntityMovement(Transform transform, CustomPhysics physicComponent)
        {
            _transform = transform;
            _physicComponent = physicComponent;
        }

        public void Move()
        {
            // _transform.Translate(_physicComponent.)
        }
    }
}