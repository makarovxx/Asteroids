using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class FollowPhysics : MovingPhysics
    {
        private const float DirectionUpdateInterval = 3;
        private readonly ShipPhysicsProvider _shipProvider;
        private readonly float _speed;
        private float _elapsedTime;

        public FollowPhysics(
            Transform body,
            RotationResolver rotationResolver,
            ShipPhysicsProvider shipProvider,
            float speed)
            : base(body, rotationResolver)
        {
            _shipProvider = shipProvider;
            _speed = speed;
        }

        public override void Tick(float deltaTime)
        {
            _elapsedTime -= deltaTime;

            if (_elapsedTime <= 0f)
            {
                UpdateDirection();
                _elapsedTime = DirectionUpdateInterval;
            }

            Move(deltaTime);
        }

        public void Reset() => _elapsedTime = DirectionUpdateInterval;

        private void UpdateDirection()
        {
            var target = _shipProvider.PhysicsTarget;

            if (!target.IsActive)
                return;

            Vector2 direction =
                (target.Position - Position).normalized;

            SetVelocity(direction * _speed);
        }
    }
}