using System.Collections.Generic;
using Project.Scripts.InputManageSystem;
using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
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
            if (!_directions.TryGetValue(direction, out Vector2 dir))
                return float.NaN;

            float radians = GetRadians(dir);
            return ToDegrees(radians);
        }
        
        public float GetAngle(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return float.NaN;

            float radians = GetRadians(direction);
            return ToDegrees(radians);
        }
        
        private float GetRadians(Vector2 dir) => Mathf.Atan2(dir.y, dir.x);

        private float ToDegrees(float radians) => radians * Mathf.Rad2Deg;
    }
}