using Project.Scripts.Core.CustomPhysics;
using UnityEngine;
using Zenject;

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
    }
}