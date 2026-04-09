using Project.Scripts.Core.InputManageSystem;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class ShipRotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 720f;

        private DesktopInput _input;
        private RotationResolver _rotationResolver;
        
        public Vector2 Forward => transform.right;
        
        private void Awake()
        {
            _rotationResolver = new RotationResolver();
            _input = new DesktopInput();
        }

        private void Update()
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
    }
}