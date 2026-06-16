using Project.Scripts.Core.CustomPhysics;

namespace Project.Scripts.Entities.Projectile
{
    public class Bullet : Entity<SolidPhysics>
    {
        public override EntityType EntityType => EntityType.Bullet;
    }
}