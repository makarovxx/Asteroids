using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities.Enemies.Ufo;
using Project.Scripts.Gameplay.Utilities.World;
using Project.Scripts.Infrastructure.Configs.SerializableData;

namespace Project.Scripts.Infrastructure.EntityFactories
{
    public sealed class UfoFactory : EntityFactory<Ufo, FollowPhysics>
    {
        private readonly UfoData _ufoData;

        public UfoFactory(Ufo prefab, PhysicsSystem physicsSystem, UfoData ufoData) : base(prefab, physicsSystem)
        {
            _ufoData = ufoData;
        }

        protected override FollowPhysics CreatePhysics(Ufo entity)
        {
            return Container.Instantiate<FollowPhysics>(new object[]
            {
                entity.transform,
                _ufoData.Speed,
                _ufoData.DirectionUpdateInterval
            });
        }
    }
}