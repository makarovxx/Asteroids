using System;
using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.Health
{
    public class ShipHealth : IInitializable, IDisposable
    {
        public event Action<int> OnHealthChanged;
        private readonly SignalBus _signalBus;
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
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}