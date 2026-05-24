using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Plugins;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Entities
{
    public class Asteroid : MonoBehaviour, ICreatable
    {
        private SolidPhysics _physicsComponent;
        
        [Inject]
        private void Construct(SolidPhysics physicsComponent)
        {
            _physicsComponent = physicsComponent;
        }

        private void FixedUpdate()
        {
            _physicsComponent?.FixedTick();
        }

        private void OnDisable()
        {
            _physicsComponent?.StopMove();
        }

        public void Spawn(Vector2 position, Vector2 direction, float speed)
        {
            transform.position = position;
            _physicsComponent.Launch(direction, speed);
        }

        public void StopMove()
        {
            _physicsComponent.StopMove();
        }
    }
}
