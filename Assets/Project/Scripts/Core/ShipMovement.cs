using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float damping = 0.8f;

    [SerializeField] private ShipRotation rotation;

    private ShipInput input;
    private Vector2 velocity;

    private void Awake()
    {
        input = new ShipInput();
    }

    private void Update()
    {
        if (input.IsThrusting())
            Accelerate();
        else
            ApplyDamping();

        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    private void Accelerate()
    {
        velocity += rotation.Forward * acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
    }

    private void ApplyDamping()
    {
        velocity = Vector2.Lerp(
            velocity,
            Vector2.zero,
            damping * Time.deltaTime
        );
    }
}