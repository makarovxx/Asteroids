using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class PhysicsSystem : IFixedTickable
    {
        private readonly List<IPhysics> _physicsObjects;
        private readonly WorldBoundsTeleport _worldBoundsTeleport;

        public PhysicsSystem(WorldBoundsTeleport worldBoundsTeleport)
        {
            _worldBoundsTeleport = worldBoundsTeleport;
            _physicsObjects = new List<IPhysics>();
        }
        
        void IFixedTickable.FixedTick()
        {
            float deltaTime = Time.fixedDeltaTime;

            TickMovement(deltaTime);
            TickBoundsCheck();
        }
        
        public void Register(IPhysics physicsObject)
        {
            if (_physicsObjects.Contains(physicsObject))
                return;

            _physicsObjects.Add(physicsObject);
        }

        private void TickMovement(float deltaTime)
        {
            for (int i = 0; i < _physicsObjects.Count; i++)
            {
                IPhysics physics =
                    _physicsObjects[i];

                if (!physics.IsActive)
                    continue;

                physics.Tick(deltaTime);
            }
        }
        
        private void TickBoundsCheck()
        {
            for (int i = 0; i < _physicsObjects.Count; i++)
            {
                IPhysics physics =
                    _physicsObjects[i];

                if (!physics.IsActive)
                    continue;

                _worldBoundsTeleport.TeleportIfOutOfBounds(physics);
            }
        }
    }
}
