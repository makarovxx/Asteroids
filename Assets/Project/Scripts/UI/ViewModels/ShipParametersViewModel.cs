using System;
using MVVM;
using Project.Scripts.Gameplay.Utilities.ShipParameters;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.ViewModels
{
    public sealed class ShipParametersViewModel : IInitializable, IDisposable
    {
        [Data("Position")]
        public readonly ReactiveProperty<string> CurrentPosition = new();
        [Data("Rotation")]
        public readonly ReactiveProperty<string> CurrentRotation = new();
        
        private readonly ShipParameters _shipParameters;

        public ShipParametersViewModel(ShipParameters shipParameters)
        {
            _shipParameters = shipParameters;
        }

        public void Initialize()
        {
            _shipParameters.OnPositionChanged += OnPositionChanged;
            _shipParameters.OnRotationChanged += OnRotationChanged;
        }

        public void Dispose()
        {
            _shipParameters.OnPositionChanged -= OnPositionChanged;
            _shipParameters.OnRotationChanged -= OnRotationChanged;
        }

        private void OnRotationChanged(float obj)
        {
            CurrentRotation.Value = obj.ToString("F1");
        }

        private void OnPositionChanged(Vector2 newPosition)
        {
            CurrentPosition.Value = "X:" + newPosition.x.ToString("F1") + "\n" + "Y:" + newPosition.y.ToString("F1");
            // CurrentPosition.Value = newPosition.x.ToString("F1") + "," + newPosition.y.ToString("F1");
        }
    }
}