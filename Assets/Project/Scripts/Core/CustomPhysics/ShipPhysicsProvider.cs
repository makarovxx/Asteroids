namespace Project.Scripts.Core.CustomPhysics
{
    public sealed class ShipPhysicsProvider
    {
        public ShipPhysics PhysicsTarget { get; private set; }
        
        public void Init(ShipPhysics physics)
        {
            PhysicsTarget = physics;
        }
    }
}