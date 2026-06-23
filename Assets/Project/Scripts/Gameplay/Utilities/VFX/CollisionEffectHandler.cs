using Project.Scripts.Gameplay.Entities;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.VFX
{
    public sealed class CollisionEffectHandler
    {
        [Inject] private readonly SignalBus _signalBus;
        private Entity _owner;
        private ParticleSystem _collisionParticles;

        public void BindOwner(Entity owner)
        {
            _owner = owner;
            _owner.TryGetComponent(out _collisionParticles);
            Subscribe();
        }

        public void UnBindOwner()
        {
            _owner = null;
            Unsubscribe();
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<ShipCollisionEnemy>(HandleCollision);
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<ShipCollisionEnemy>(HandleCollision);
        }

        private void HandleCollision(ShipCollisionEnemy obj)
        {
            if (_owner.Equals(obj.HitBy))
                PlayEffect();
        }

        private void PlayEffect()
        {
            _collisionParticles?.Play();
        }
    }
}