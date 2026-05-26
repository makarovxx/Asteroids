using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class PhysicsSystem : IFixedTickable
    {
        private readonly float _deltaTime = Time.fixedDeltaTime;

        private readonly List<IPhysicsObject> _physicsObjects;

        public PhysicsSystem()
        {
            _physicsObjects = new List<IPhysicsObject>();
        }

        public void Register(IPhysicsObject physicsObject)
        {
            if (_physicsObjects.Contains(physicsObject))
                return;

            _physicsObjects.Add(physicsObject);
        }

        public void Unregister(IPhysicsObject physicsObject)
        {
            _physicsObjects.Remove(physicsObject);
        }

        void IFixedTickable.FixedTick()
        {
            for (int i = 0; i < _physicsObjects.Count; i++)
            {
                IPhysicsObject physicsObject =
                    _physicsObjects[i];

                if (!physicsObject.IsActive)
                    continue;

                physicsObject.Tick(_deltaTime);
            }
        }
    }
}