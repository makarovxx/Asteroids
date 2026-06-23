using System;
using MVVM;
using Project.Scripts.Gameplay.Utilities.Health;
using Project.Scripts.Gameplay.Utilities.ShipParameters;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.ViewModels
{
    public sealed class ShipParametersViewModel : IInitializable, IDisposable
    {
        private const string RotationLabel = "Rotation:\n";
        private const string XPosLabel = "X:";
        private const string YPosLabel = "\nY:";

        [Data("Health")] 
        public readonly ReactiveProperty<string> CurrentHealth = new();

        [Data("Position")]
        public readonly ReactiveProperty<string> CurrentPosition = new();

        [Data("Rotation")]
        public readonly ReactiveProperty<string> CurrentRotation = new();

        [Data("Speed")]
        public readonly ReactiveProperty<string> CurrentSpeed = new();


        private readonly ShipHealth _shipHealth;
        private readonly ShipBodyParameters _shipBodyParameters;

        public ShipParametersViewModel(ShipBodyParameters shipBodyParameters, ShipHealth shipHealth)
        {
            _shipBodyParameters = shipBodyParameters;
            _shipHealth = shipHealth;
        }

        public void Initialize()
        {
            OnHealthChanged(_shipHealth.CurrentHealth);
            _shipHealth.OnHealthChanged += OnHealthChanged;
            _shipBodyParameters.OnPositionChanged += OnPositionChanged;
            _shipBodyParameters.OnRotationChanged += OnRotationChanged;
            _shipBodyParameters.OnSpeedChanged += OnSpeedChanged;
        }

        public void Dispose()
        {
            _shipHealth.OnHealthChanged -= OnHealthChanged;
            _shipBodyParameters.OnPositionChanged -= OnPositionChanged;
            _shipBodyParameters.OnRotationChanged -= OnRotationChanged;
            _shipBodyParameters.OnSpeedChanged -= OnSpeedChanged;
        }
        
        private void OnHealthChanged(int currentHealth)
        {
            CurrentHealth.Value = currentHealth.ToString();
        }
        
        private void OnRotationChanged(float value)
        {
            CurrentRotation.Value = RotationLabel + value.ToString("F0");
        }

        private void OnPositionChanged(Vector2 newPosition)
        {
            CurrentPosition.Value = XPosLabel + newPosition.x.ToString("F1") + YPosLabel + newPosition.y.ToString("F1");
        }

        private void OnSpeedChanged(float value)
        {
            CurrentSpeed.Value = value.ToString("F1");
        }
    }
}