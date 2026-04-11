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
    }
}