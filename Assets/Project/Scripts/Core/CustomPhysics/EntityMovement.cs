using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class EntityMovement
    {
        public Vector2 Velocity { get; private set; }
        protected readonly Transform Body;
        private readonly float _maxSpeed = 12f;

        public EntityMovement(Transform body, Vector2 startVelocity)
        {
            Body = body;
            Velocity = startVelocity;
        }

        public Vector2 GetPosition() => Body.position;
        
        public void SetPosition(Vector2 position) => Body.position = position;
        
        public void SetVelocity(Vector2 velocity) => Velocity = Vector2.ClampMagnitude(velocity, _maxSpeed);

        public void StopMove() => Velocity = Vector2.zero;

        protected virtual void Move() => Body.Translate(Velocity * Time.deltaTime, Space.World);
    }
}