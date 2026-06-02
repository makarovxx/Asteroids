using Project.Scripts.Configs;
using Project.Scripts.Entities.Ship;
using Project.Scripts.Plugins;
using Zenject;

namespace Project.Scripts.EntityFactories
{
    public class ShipSpawner : IInitializable
    {
        private readonly ICreator<Ship> _shipCreator;
        private readonly PlayerConfig _playerConfig;

        public ShipSpawner(ICreator<Ship> shipCreator, PlayerConfig playerConfig)
        {
            _shipCreator = shipCreator;
            _playerConfig = playerConfig;
        }

        public void Initialize()
        {
            Ship ship = _shipCreator.Create();

            ship.transform.position = _playerConfig.SpawnPoint;

            if (_playerConfig.Container != null)
                ship.transform.SetParent(_playerConfig.Container);
        }
    }

    public class TestFactory : PlaceholderFactory<Ship>
    {
        
    }
}