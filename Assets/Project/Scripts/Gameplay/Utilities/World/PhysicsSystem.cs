using System;
using System.Collections.Generic;
using Project.Scripts.Core.CustomPhysics;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.World
{
    public sealed class PhysicsSystem : IInitializable, IDisposable, IFixedTickable
    {
        private readonly List<IPhysics> _physicsObjects;
        private readonly WorldBoundsTeleport _worldBoundsTeleport;
        private readonly float _deltaTime = Time.fixedDeltaTime;

        public PhysicsSystem(WorldBoundsTeleport worldBoundsTeleport)
        {
            _worldBoundsTeleport = worldBoundsTeleport;
            _physicsObjects = new List<IPhysics>();
        }

        void IInitializable.Initialize()
        {
        }

        void IDisposable.Dispose()
        {
        }

        void IFixedTickable.FixedTick()
        {
            TickMovement(_deltaTime);
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