namespace Project.Scripts.Gameplay.Entities.Projectile
{
    public sealed class Laser : Entity
    {
        public override EntityType EntityType => EntityType.Laser;
        public override CollisionHandlePriority CollisionPriority => CollisionHandlePriority.High;
    }
}
