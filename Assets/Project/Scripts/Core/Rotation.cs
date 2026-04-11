using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class Rotation
    {
        protected Transform Body;
        
        private RotationResolver _rotationResolver;
        private readonly float _rotationSpeed;

        public Rotation(Transform body, float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
            Body = body;
        }

        public void RotateSmoothly(DirectionRotation direction)
        {
            if (direction == DirectionRotation.None)  return;

            float targetAngle = _rotationResolver.GetAngle(direction);

            float newAngle = Mathf.MoveTowardsAngle(
                Body.eulerAngles.z,
                targetAngle,
                _rotationSpeed * Time.deltaTime
            );

            Body.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }
    }
}