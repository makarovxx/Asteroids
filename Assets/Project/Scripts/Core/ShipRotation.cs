using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 720f;

    private ShipInput input;
    private RotationResolver rotationResolver;

    private void Awake()
    {
        var directionProvider = new DirectionProvider();
        rotationResolver = new RotationResolver(directionProvider);
        input = new ShipInput();
    }

    private void Update()
    {
        RotationDirection direction = input.GetRotationDirection();

        if (direction == RotationDirection.None)
            return;

        float targetAngle = rotationResolver.GetAngle(direction);

        float newAngle = Mathf.MoveTowardsAngle(
            transform.eulerAngles.z,
            targetAngle,
            rotationSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    // Отдаём текущее направление носа
    public Vector2 Forward => transform.right;
}

public enum RotationDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class ShipInput
{
    public RotationDirection GetRotationDirection()
    {
        if (Input.GetKey(KeyCode.W)) return RotationDirection.Up;
        if (Input.GetKey(KeyCode.S)) return RotationDirection.Down;
        if (Input.GetKey(KeyCode.A)) return RotationDirection.Left;
        if (Input.GetKey(KeyCode.D)) return RotationDirection.Right;

        return RotationDirection.None;
    }

    public bool IsThrusting()
    {
        return Input.GetKey(KeyCode.Space);
    }
}

public class DirectionProvider
{
    private readonly Dictionary<RotationDirection, Vector2> directions =
        new()
        {
            { RotationDirection.Up, Vector2.up },
            { RotationDirection.Down, Vector2.down },
            { RotationDirection.Left, Vector2.left },
            { RotationDirection.Right, Vector2.right }
        };

    public Vector2 GetVector(RotationDirection direction)
    {
        if (direction == RotationDirection.None)
            return Vector2.zero;

        return directions[direction];
    }
}


public class RotationResolver
{
    private readonly DirectionProvider directionProvider;

    public RotationResolver(DirectionProvider provider)
    {
        directionProvider = provider;
    }

    public float GetAngle(RotationDirection direction)
    {
        Vector2 dir = directionProvider.GetVector(direction);

        if (dir == Vector2.zero)
            return float.NaN;

        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}