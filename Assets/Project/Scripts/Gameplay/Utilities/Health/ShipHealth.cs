using System;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.Health
{
    public class ShipHealth : IInitializable, IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private readonly int _initialHealth;
        public event Action<int> OnHealthChanged;
        public int CurrentHealth { get; private set; }
        
        [Inject]
        public ShipHealth(ShipData shipData)
        {
            CurrentHealth = _initialHealth = shipData.AmountHealth;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RestartGameSignal>(ResetHealth);
            _signalBus.Subscribe<ShipCollisionEnemy>(TakeDamage);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RestartGameSignal>(ResetHealth);
            _signalBus.Unsubscribe<ShipCollisionEnemy>(TakeDamage);
        }

        private void TakeDamage()
        {
            CurrentHealth--;

            OnHealthChanged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                _signalBus.Fire<GameOverSignal>();
                return;
            }

            _signalBus.Fire<ShipDamageSignal>();
        }

        private void ResetHealth()
        {
            CurrentHealth = _initialHealth;
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}
