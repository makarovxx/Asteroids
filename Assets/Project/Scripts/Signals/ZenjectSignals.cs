using Project.Scripts.Gameplay.Entities;

namespace Project.Scripts.Signals
{
    public class ShipCollisionEnemy
    {
        public Entity HitBy { get; }

        public ShipCollisionEnemy(Entity hitBy)
        {
            HitBy = hitBy;
        }
    }
    
    public class WeaponHitEnemy
    {
        public Entity EnemyDestroyed { get; }
        public Entity HitBy { get; }

        public WeaponHitEnemy(Entity enemyDestroyed, Entity hitBy)
        {
            EnemyDestroyed = enemyDestroyed;
            HitBy = hitBy;
        }
    }

    public class GameOverSignal
    {
    }

    public class ShipDamageSignal
    {
    }


    public class InvulnerableEndedSignal
    {
    }

    public class RestartGameSignal
    {
    }
    
    public class PauseGameSignal{}
    
    public class ResumeGameSignal{}
}
