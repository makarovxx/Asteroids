using Project.Scripts.Gameplay.Entities;

namespace Project.Scripts.Signals
{
    public class ShipHitEnemy
    {
        public Entity HitBy { get; }

        public ShipHitEnemy(Entity hitBy)
        {
            HitBy = hitBy;
        }
    }
    
    public class EnemyHitByWeaponSignal
    {
        public Entity EnemyDestroyed { get; }
        public Entity HitBy { get; }

        public EnemyHitByWeaponSignal(Entity enemyDestroyed, Entity hitBy)
        {
            EnemyDestroyed = enemyDestroyed;
            HitBy = hitBy;
        }
    }

    public class ShipDeathSignal
    {
    }
}