using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public interface IPhysics
    {
        bool IsActive { get; }
        Vector2 Position { get; set; }
        float Rotation { get; }
        public void Tick(float deltaTime);
    }
}