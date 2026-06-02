using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public abstract class PhysicBase : IPhysics
    {
        public Vector2 Position
        {
            get => Body.position;
            set => Body.position = value;
        }

        public float Rotation
        {
            get => Body.rotation.eulerAngles.z;
            set => Body.rotation = Quaternion.Euler(0, 0, value);
        }
        
        public bool IsActive => Body.gameObject.activeSelf;
        
        protected Transform Body;

        protected PhysicBase(Transform body)
        {
            Body = body;
        }

        public abstract void Tick(float deltaTime);
    }
}
