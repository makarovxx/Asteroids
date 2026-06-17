using System.Collections.Generic;
using Project.Scripts.Gameplay.Entities;

namespace Project.Scripts.Core.CollisionSystem
{
    public enum CollisionResult
    {
        Ignore, 
        Bounce, 
        Straight 
    }
    
    public class CollisionMatrix
    {
        private readonly Dictionary<(EntityType, EntityType), CollisionResult> _matrix;

        public CollisionMatrix()
        {
            _matrix = new Dictionary<(EntityType, EntityType), CollisionResult>
            {
                { Pair(EntityType.Ship, EntityType.LargeAsteroid), CollisionResult.Bounce },
                { Pair(EntityType.Ship, EntityType.MediumAsteroid), CollisionResult.Bounce },
                { Pair(EntityType.Ship, EntityType.SmallAsteroid), CollisionResult.Bounce },
                { Pair(EntityType.Ship, EntityType.Ufo), CollisionResult.Bounce },

                { Pair(EntityType.Bullet, EntityType.LargeAsteroid), CollisionResult.Straight },
                { Pair(EntityType.Bullet, EntityType.MediumAsteroid), CollisionResult.Straight },
                { Pair(EntityType.Bullet, EntityType.SmallAsteroid), CollisionResult.Straight },
                { Pair(EntityType.Bullet, EntityType.Ufo), CollisionResult.Straight },

                { Pair(EntityType.Laser, EntityType.LargeAsteroid), CollisionResult.Straight },
                { Pair(EntityType.Laser, EntityType.MediumAsteroid), CollisionResult.Straight },
                { Pair(EntityType.Laser, EntityType.SmallAsteroid), CollisionResult.Straight },
                { Pair(EntityType.Laser, EntityType.Ufo), CollisionResult.Straight },
            };
        }

        public CollisionResult GetResult(EntityType a, EntityType b)
        {
            return _matrix.GetValueOrDefault(Pair(a, b), CollisionResult.Ignore);
        }
        
        private (EntityType, EntityType) Pair(EntityType a, EntityType b)
        {
            return a <= b ? (a, b) : (b, a);
        }
    }
}