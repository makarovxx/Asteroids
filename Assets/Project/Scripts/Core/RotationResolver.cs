using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Core
{
    public enum DirectionRotation
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }

    public class RotationResolver
    {
        private readonly Dictionary<DirectionRotation, Vector2> _directions = new()
        {
            { DirectionRotation.Up, Vector2.up },
            { DirectionRotation.Down, Vector2.down },
            { DirectionRotation.Left, Vector2.left },
            { DirectionRotation.Right, Vector2.right }
        };
        
        public float GetAngle(DirectionRotation direction)
        {
            Vector2 dir = GetVector(direction);

            if (dir == Vector2.zero)
                return float.NaN;

            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
        
        private Vector2 GetVector(DirectionRotation direction)
        {
            if (direction == DirectionRotation.None)
                return Vector2.zero;

            return _directions[direction];
        }

        
    }
}