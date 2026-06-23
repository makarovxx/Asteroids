using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class SolidPhysics : PhysicBase, IMovingPhysics
    {
        protected const float MaxEntitySpeed = 5f;
        public Vector2 Velocity { get; protected set; }
        public float CurrentSpeed => Velocity.magnitude;
        public Vector2 DirectionBody => Body.right;
        
        protected SolidPhysics(Transform body) : base(body)
        {
        }

        public override void Tick(float deltaTime) => Move(deltaTime);

        public void SetVelocity(Vector2 velocity) => Velocity = Vector2.ClampMagnitude(velocity, MaxEntitySpeed);

        public void StopMove() => Velocity = Vector2.zero;
        
        protected void Move(float deltaTime) => Body.Translate(Velocity * deltaTime, Space.World);
    }
}
