using System;
using Project.Scripts.Core.TickableSystem;
using Project.Scripts.Gameplay.Entities.Projectile;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.Weapons
{
    public sealed class LaserController : IInitializable, IDisposable, IBehaviourTickable
    {
        private readonly LaserProvider _laserProvider;
        private readonly InputManager _input;
        
        private readonly float _laserActiveTime;
        private readonly float _rechargeTime;
        private readonly int _maxCharges;
        
        private Laser _laser;

        private float _activeTimeRemaining;
        private float _rechargeTimeRemaining;

        public event Action<int> OnCurrentChargesChanged;
        public event Action<float> OnRechargeTimeChanged;

        public int CurrentCharges { get; private set; }

        public float RechargeTimeRemaining => CurrentCharges < _maxCharges
            ? Mathf.Max(0f, _rechargeTimeRemaining)
            : 0f;

        public LaserController(LaserData data, LaserProvider laserProvider, InputManager input)
        {
            _laserProvider = laserProvider;
            _input = input;
            _laserActiveTime = data.ActiveTime;
            _maxCharges = data.MaxCharges;
            CurrentCharges = data.MaxCharges;
            _rechargeTime = data.RechargeTime;
        }

        public void Initialize()
        {
            _laser = _laserProvider?.Target;
        }

        public void Dispose()
        {
        }

        public void Tick(float deltaTime)
        {
            TickLaserLifetime(deltaTime);
            TickRecharge(deltaTime);

            if (_input.IsLaserFire())
                TryFire();
        }

        private void TryFire()
        {
            if (CurrentCharges <= 0 || _laser.gameObject.activeSelf)
                return;

            CurrentCharges--;
            OnCurrentChargesChanged?.Invoke(CurrentCharges);

            _activeTimeRemaining = _laserActiveTime;
            _laser.gameObject.SetActive(true);
        }

        private void TickLaserLifetime(float deltaTime)
        {
            _activeTimeRemaining -= deltaTime;

            if (_activeTimeRemaining <= 0f)
                _laser.gameObject.SetActive(false);
        }

        private void TickRecharge(float deltaTime)
        {
            if (CurrentCharges >= _maxCharges)
            {
                _rechargeTimeRemaining = _rechargeTime;
                OnRechargeTimeChanged?.Invoke(RechargeTimeRemaining);
                return;
            }

            _rechargeTimeRemaining -= deltaTime;
            OnRechargeTimeChanged?.Invoke(RechargeTimeRemaining);

            if (!(_rechargeTimeRemaining <= 0f) || CurrentCharges >= _maxCharges)
                return;

            CurrentCharges++;
            OnCurrentChargesChanged?.Invoke(CurrentCharges);
            _rechargeTimeRemaining += _rechargeTime;
        }
    }
}