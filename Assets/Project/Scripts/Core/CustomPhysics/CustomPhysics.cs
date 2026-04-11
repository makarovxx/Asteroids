// using UnityEngine;
//
// namespace Project.Scripts.Core.CustomPhysics
// {
//     public class CustomPhysics : MonoBehaviour
//     {
//         [SerializeField] private MovementType movementType;
//         [SerializeField] private float maxSpeed      = 12f;
//         [SerializeField] private float acceleration  = 8f;
//         [SerializeField] private float damping       = 0.8f;
//         [SerializeField] private float rotationSpeed = 720f;
//
//         private Vector2 _velocity;
//         private RotationResolver _rotationResolver;
//         
//         public Vector2 Forward => transform.right;
//         public MovementType MovementType => movementType;
//
//         private void Awake()
//         {
//             _rotationResolver = new RotationResolver();
//         }
//
//         public void Accelerate()
//         {
//             if (movementType != MovementType.Dynamic) return;
//
//             _velocity += Forward * acceleration * Time.deltaTime;
//             _velocity  = Vector2.ClampMagnitude(_velocity, maxSpeed);
//         }
//
//         public void ApplyDamping()
//         {
//             if (movementType != MovementType.Dynamic) return;
//
//             _velocity = Vector2.Lerp(_velocity, Vector2.zero, damping * Time.deltaTime);
//         }
//
//         public void RotateSmoothly(DirectionRotation direction)
//         {
//             if (movementType != MovementType.Dynamic) return;
//             if (direction == DirectionRotation.None)  return;
//
//             float targetAngle = _rotationResolver.GetAngle(direction);
//
//             float newAngle = Mathf.MoveTowardsAngle(
//                 transform.eulerAngles.z,
//                 targetAngle,
//                 rotationSpeed * Time.deltaTime
//             );
//
//             transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
//         }
//
//         public void SetVelocity(Vector2 velocity)
//         {
//             if (movementType != MovementType.Solid) return;
//
//             _velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
//         }
//         
//         [ContextMenu("ApplyVelocity")]
//         private void ApplyVelocity()
//         {
//             _velocity = Vector2.up;
//         }
//
//         public void Update()
//         {
//             transform.Translate(_velocity * Time.deltaTime, Space.World);
//         }
//     }
// }