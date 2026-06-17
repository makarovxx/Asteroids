using Project.Scripts.Gameplay.Entities;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CollisionSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionDetector : MonoBehaviour
    {
        private Entity _owner;
        private CollisionSystem _collisionSystem;

        [Inject]
        public void Construct(CollisionSystem collisionSystem)
        {
            _collisionSystem = collisionSystem;
            _owner = GetComponent<Entity>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<CollisionDetector>(out var otherDetector))
                return;
            
            if (!_collisionSystem.CanCollide(_owner, otherDetector._owner))
                return;

            _collisionSystem.HandleCollision(_owner, otherDetector._owner);
        }
    }
}