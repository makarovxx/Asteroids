using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public interface IMovingPhysics : IPhysics
    {
        Vector2 Velocity { get; }
        Vector2 DirectionBodyDefault { get; }
        void Move(float deltaTime);
        void SetVelocity(Vector2 velocity);
        void StopMove();
        void SetRotation(float angle);
    }
}