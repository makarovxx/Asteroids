using Project.Scripts.Gameplay.Entities.Projectile;
using Project.Scripts.Gameplay.Entities.Ship;
using Zenject;

namespace Project.Scripts.Infrastructure.EntityFactories
{
    public class LaserSpawner : IInitializable
    {
        private readonly Laser _prefab;
        private readonly LaserProvider _laserProvider;
        private readonly ShipProvider _shipProvider;
        [Inject] private readonly DiContainer _container;
        
        [Inject]
        public LaserSpawner(LaserProvider laserProvider, ShipProvider shipProvider, Laser prefab)
        {
            _laserProvider = laserProvider;
            _shipProvider = shipProvider;
            _prefab = prefab;
        }
        
        private void Create()
        {
            var laser = _container.InstantiatePrefabForComponent<Laser>(_prefab,_shipProvider.Ship.transform);
            _laserProvider?.Init(laser);
        }

        public void Initialize()
        {
            Create();
        }
    }
}