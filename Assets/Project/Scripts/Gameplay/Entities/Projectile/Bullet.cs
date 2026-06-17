using System.Threading;
using Project.Scripts.Core.CustomPhysics;

namespace Project.Scripts.Gameplay.Entities.Projectile
{
    public class Bullet : Entity<SolidPhysics>, ICollidable
    {
        public override EntityType EntityType => EntityType.Bullet;
        public override CollisionHandlePriority CollisionPriority => CollisionHandlePriority.High;
        public CancellationTokenSource LifeTimeCts { get; private set; }

        public void OnEnable()
        {
            LifeTimeCts?.Cancel();
            LifeTimeCts?.Dispose();

            LifeTimeCts = new CancellationTokenSource();
        }

        public void OnDisable()
        {
            LifeTimeCts?.Cancel();
        }
    }
}