using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Core.CustomPhysics;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.World
{
    public sealed class PhysicsSystem
    {
        [Inject] private  readonly SignalBus _signalBus;
        private readonly List<IPhysics> _physicsObjects;
        private readonly WorldBoundsTeleport _worldBoundsTeleport;

        public PhysicsSystem(WorldBoundsTeleport worldBoundsTeleport)
        {
            _worldBoundsTeleport = worldBoundsTeleport;
            _physicsObjects = new List<IPhysics>();
        }

        public void Register(IMovingPhysics physicsObject)
        {
            if (_physicsObjects.Contains(physicsObject))
                return;

            _physicsObjects.Add(physicsObject);
        }

        public void Tick(float fixedDeltaTime)
        {
            TickMovement(fixedDeltaTime);
            TickBoundsCheck();
        }

        private void TickMovement(float deltaTime)
        {
            foreach (IPhysics physics in _physicsObjects)
            {
                if (!physics.IsActive)
                    continue;

                physics.Tick(deltaTime);
            }
        }

        private void TickBoundsCheck()
        {
            foreach (var physics in _physicsObjects.Cast<IMovingPhysics>().Where(physics => physics.IsActive))
            {
                _worldBoundsTeleport.TeleportIfOutOfBounds(physics);
            }
        }
    }
}