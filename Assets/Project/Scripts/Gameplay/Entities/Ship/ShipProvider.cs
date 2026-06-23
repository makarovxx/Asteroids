using Project.Scripts.Core.CustomPhysics;
using Zenject;

namespace Project.Scripts.Gameplay.Entities.Ship
{
    public sealed class ShipProvider
    {
        public Ship Ship { get; private set; }
        public ShipPhysics Physics { get; private set; }
        
        public void Init(Ship ship, ShipPhysics physics)
        {
            Ship = ship;
            Physics = physics;
        }
    }
}
