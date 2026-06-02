using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class FollowPhysics : MovingPhysics
    {
        private readonly IPhysics _target;
        private readonly float _speed;
        
        [Inject]
        public FollowPhysics(Transform body, RotationResolver rotationResolver, IPhysics target, float speed) : base(
            body, rotationResolver)
        {
            _target = target;
            _speed = speed;
        }

        public override void Move(float deltaTime)
        {
            if (_target.IsActive)
            {
                Vector2 direction =
                    (_target.Position - Position).normalized;

                SetVelocity(direction * _speed);

                base.Move(deltaTime);
            }
        }
    }
}