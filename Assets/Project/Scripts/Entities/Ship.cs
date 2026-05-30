using System;
using Project.Scripts.Core.CustomPhysics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts.Entities
{
    public class Ship : MonoBehaviour
    {
        private ShipPhysics _shipPhysics;
        
        [Inject]
        private void Construct(ShipPhysics shipPhysics)
        {
            _shipPhysics = shipPhysics;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 direction =
                Random.insideUnitCircle.normalized;
            _shipPhysics.SetVelocity(direction);
        }
    }
}