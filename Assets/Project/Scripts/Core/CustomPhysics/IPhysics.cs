using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public interface IPhysicsObject
    {
        bool IsActive { get; }
        Vector2 Position { get; set; }
        float Rotation { get; }
        void Tick(float deltaTime);
    }
}