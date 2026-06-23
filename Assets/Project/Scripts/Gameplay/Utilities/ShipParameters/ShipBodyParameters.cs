using System;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Core.TickableSystem;
using Project.Scripts.Gameplay.Utilities.World;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.ShipParameters
{
    public class ShipBodyParameters : IBehaviourTickable
    {
        private readonly ShipPhysicsProvider _shipPhysicsProvider;
        private readonly CameraSpaceMapper _spaceMapper;

        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnRotationChanged;
        public event Action<float> OnSpeedChanged;
        private Vector2 CurrentPosition { get; set; }
        private float CurrentRotation { get; set; }

        [Inject]
        public ShipBodyParameters(ShipPhysicsProvider shipPhysicsProvider, CameraSpaceMapper spaceMapper)
        {
            _shipPhysicsProvider = shipPhysicsProvider;
            _spaceMapper = spaceMapper;
        }

        public void Tick(float deltaTime)
        {
            PositionChanged();
            RotationChanged();
            SpeedChanged();
        }

        private void PositionChanged()
        {
            var position = _spaceMapper.WorldToViewport(_shipPhysicsProvider.PhysicsTarget.Position);
            CurrentPosition = position;
            OnPositionChanged?.Invoke(CurrentPosition);
        }

        private void RotationChanged()
        {
            CurrentRotation = _shipPhysicsProvider.PhysicsTarget.Rotation;
            OnRotationChanged?.Invoke(CurrentRotation);
        }

        private void SpeedChanged()
        {
            OnSpeedChanged?.Invoke(_shipPhysicsProvider.PhysicsTarget.CurrentSpeed);
        }
    }
}