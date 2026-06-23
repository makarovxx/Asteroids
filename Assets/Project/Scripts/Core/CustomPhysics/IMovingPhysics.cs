using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public interface IMovingPhysics : IPhysics
    {
        Vector2 Velocity { get; }
        float CurrentSpeed => Velocity.magnitude;
        void SetVelocity(Vector2 velocity);
    }
}