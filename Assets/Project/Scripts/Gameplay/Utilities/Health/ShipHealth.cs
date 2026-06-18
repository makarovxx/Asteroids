using System;
using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.Health
{
    public class ShipHealth : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        public event Action<int> OnHealthChanged;
        public int CurrentHealth { get; private set; }
        
        [Inject]
        public ShipHealth(int currentHealth, SignalBus signalBus)
        {
            CurrentHealth = currentHealth;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ShipHitEnemy>(TakeDamage);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ShipHitEnemy>(TakeDamage);
        }

        private void TakeDamage()
        {
            CurrentHealth--;
            if (CurrentHealth <= 0)
                _signalBus.Fire<ShipDeathSignal>();
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}