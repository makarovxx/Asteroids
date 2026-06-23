using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public sealed class FollowPhysics : SolidPhysics
    {
        private readonly ShipPhysicsProvider _shipProvider;
        private readonly float  _directionUpdateInterval;
        private readonly float _speed;
        private float _elapsedTime;
        
        public FollowPhysics(Transform body, float speed, float directionUpdateInterval,
            ShipPhysicsProvider shipProvider) : base(body)
        {
            _shipProvider = shipProvider;
            _speed = speed;
            _directionUpdateInterval = directionUpdateInterval;
        }

        public override void Tick(float deltaTime)
        {
            _elapsedTime -= deltaTime;

            if (_elapsedTime <= 0f)
            {
                UpdateDirection();
                _elapsedTime = _directionUpdateInterval;
            }

            Move(deltaTime);
        }

        public void Reset() => _elapsedTime = _directionUpdateInterval;

        public void TrySetTarget()
        {
            if (_shipProvider?.PhysicsTarget != null)
                UpdateDirection();
        }

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