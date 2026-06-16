using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class FollowPhysics : MovingPhysics
    {
        private readonly ShipPhysicsProvider _shipProvider;
        private readonly float _speed;

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
            TryUpdateDirection();
            Move(deltaTime);
        }

        private void TryUpdateDirection()
        {
            IPhysics target = _shipProvider.PhysicsTarget;

            if (!target.IsActive)
                return;

            Vector2 direction = (target.Position - Position).normalized;
            
            if (direction == Vector2.zero)
                return;

            SetVelocity(direction * _speed);
        }
    }
}