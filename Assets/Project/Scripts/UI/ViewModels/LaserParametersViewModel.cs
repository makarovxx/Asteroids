using System;
using MVVM;
using Project.Scripts.Gameplay.Utilities.Weapons;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.ViewModels
{
    public sealed class LaserParametersViewModel : IInitializable, IDisposable
    {
        [Data("Charges")] 
        public readonly ReactiveProperty<string> LaserCharges = new();
        [Data("RechargeProgress")] 
        public readonly ReactiveProperty<float> RechargeTimeRemaining = new();

        private readonly LaserController _laserController;
        private readonly LaserData _laserData;

        public LaserParametersViewModel(LaserController laserController, LaserData laserData)
        {
            _laserController = laserController;
            _laserData = laserData;
        }

        public void Initialize()
        {
            OnLaserChargesChanged(_laserController.CurrentCharges);
            _laserController.OnCurrentChargesChanged += OnLaserChargesChanged;
            _laserController.OnRechargeTimeChanged += OnRechargeTimeChanged;
        }

        public void Dispose()
        {
            _laserController.OnCurrentChargesChanged -= OnLaserChargesChanged;
            _laserController.OnRechargeTimeChanged -= OnRechargeTimeChanged;
        }

        private void OnLaserChargesChanged(int charges)
        {
            LaserCharges.Value = charges.ToString();
        }

        private void OnRechargeTimeChanged(float value)
        {
            RechargeTimeRemaining.Value = Mathf.InverseLerp(_laserData.RechargeTime, 0f, value);
        }
    }
}