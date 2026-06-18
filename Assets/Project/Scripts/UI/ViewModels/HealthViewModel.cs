using System;
using MVVM;
using Project.Scripts.Gameplay.Utilities.Health;
using UniRx;
using Zenject;

namespace Project.Scripts.UI.ViewModels
{
    public sealed class HealthViewModel : IInitializable, IDisposable
    {
        [Data("Health")] 
        public readonly ReactiveProperty<string> CurrentHealth = new();

        private readonly ShipHealth _shipHealth;

        public HealthViewModel(ShipHealth shipHealth)
        {
            _shipHealth = shipHealth;
        }

        public void Initialize()
        {
            OnHealthChanged(_shipHealth.CurrentHealth);
            _shipHealth.OnHealthChanged += OnHealthChanged;
        }

        public void Dispose()
        {
            _shipHealth.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int currentHealth)
        {
            CurrentHealth.Value = currentHealth.ToString();
        }
    }
}