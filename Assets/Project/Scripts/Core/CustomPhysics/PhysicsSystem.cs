using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class PhysicsSystem : IFixedTickable
    {
        private readonly float _deltaTime = Time.fixedDeltaTime;
        private readonly List<EntityPhysicsBase> _physicsObjects;
        private readonly WorldBoundsTeleport _worldBoundsTeleport;

        public PhysicsSystem(WorldBoundsTeleport worldBoundsTeleport)
        {
            _worldBoundsTeleport = worldBoundsTeleport;
            _physicsObjects = new List<EntityPhysicsBase>();
        }
        
        void IFixedTickable.FixedTick()
        {
            TickMovement();
            TickBoundsCheck();
        }
        
        public void Register(EntityPhysicsBase physicsObject)
        {
            if (_physicsObjects.Contains(physicsObject))
                return;

            _physicsObjects.Add(physicsObject);
        }

        private void TickMovement()
        {
            for (int i = 0; i < _physicsObjects.Count; i++)
            {
                EntityPhysicsBase physicsObject =
                    _physicsObjects[i];

                if (!physicsObject.IsActive)
                    continue;

                physicsObject.Tick(_deltaTime);
            }
        }
        
        private void TickBoundsCheck()
        {
            for (int i = 0; i < _physicsObjects.Count; i++)
            {
                EntityPhysicsBase physicsObject =
                    _physicsObjects[i];

                if (!physicsObject.IsActive)
                    continue;

                _worldBoundsTeleport.TeleportIfOutOfBounds(physicsObject);
            }
        }
    }
}