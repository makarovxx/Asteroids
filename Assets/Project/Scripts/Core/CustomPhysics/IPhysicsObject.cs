using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public interface IPhysicsObject
    {
        bool IsActive { get; }
        

        void Tick(float deltaTime);
    }
}