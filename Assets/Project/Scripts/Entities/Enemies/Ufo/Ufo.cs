using Project.Scripts.Core.CustomPhysics;

namespace Project.Scripts.Entities.Enemies.Ufo
{
    public class Ufo : Entity<FollowPhysics>
    {
        public override EntityType EntityType => EntityType.Ufo;
    }
}

