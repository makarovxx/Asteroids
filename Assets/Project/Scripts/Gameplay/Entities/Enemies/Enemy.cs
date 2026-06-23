using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Utilities.VFX;
using Zenject;

namespace Project.Scripts.Gameplay.Entities.Enemies
{
    public abstract class Enemy<TPhysics> : Entity<TPhysics> where TPhysics : class, IMovingPhysics
    {
        private CollisionEffectHandler _collisionVFXHandler;

        [Inject]
        private void Construct(CollisionEffectHandler collisionVFXHandler)
        {
            _collisionVFXHandler = collisionVFXHandler;
            _collisionVFXHandler.BindOwner(this);
        }

        public override EntityType EntityType { get; }
        public abstract override CollisionHandlePriority CollisionPriority { get; }

        private void OnDestroy()
        {
            _collisionVFXHandler.UnBindOwner();
        }
    }
}