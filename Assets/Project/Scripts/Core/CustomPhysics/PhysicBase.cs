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

        public float Rotation => Body.rotation.eulerAngles.z;

        public bool IsActive => Body.gameObject.activeSelf;

        protected readonly Transform Body;

        protected PhysicBase(Transform body)
        {
            Body = body;
        }

        public abstract void Tick(float deltaTime);
    }
}
