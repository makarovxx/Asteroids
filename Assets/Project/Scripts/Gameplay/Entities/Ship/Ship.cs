using Project.Scripts.Core.CustomPhysics;

namespace Project.Scripts.Gameplay.Entities.Ship
{
    public class Ship : Entity<ShipPhysics>
    {
        public override EntityType EntityType => EntityType.Ship;
        public override CollisionHandlePriority CollisionPriority => CollisionHandlePriority.High;
    }
}