using Project.Scripts.Core.CustomPhysics;

namespace Project.Scripts.Gameplay.Entities.Enemies.Ufo
{
    public class Ufo : Enemy<FollowPhysics>
    {
        public override EntityType EntityType => EntityType.Ufo;
        public override CollisionHandlePriority CollisionPriority => CollisionHandlePriority.Low;
    }
}

