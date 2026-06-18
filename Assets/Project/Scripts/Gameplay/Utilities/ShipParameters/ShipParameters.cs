using System;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Utilities.World;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.ShipParameters
{
    public class ShipParameters : ITickable
    {
        private readonly ShipPhysicsProvider _shipPhysicsProvider;
        private readonly CameraSpaceMapper _spaceMapper;

        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnRotationChanged;
        public Vector2 CurrentPosition { get; private set; }
        public float CurrentRotation { get; private set; }

        [Inject]
        public ShipParameters(ShipPhysicsProvider shipPhysicsProvider, CameraSpaceMapper spaceMapper)
        {
            _shipPhysicsProvider = shipPhysicsProvider;
            _spaceMapper = spaceMapper;
        }
        
        private void PositionChanged()
        {
            var position = _spaceMapper.WorldToViewport(_shipPhysicsProvider.PhysicsTarget.Position);
            CurrentPosition = position;
            OnPositionChanged?.Invoke(position);
        }

        private void RotationChanged()
        {
            CurrentRotation = _shipPhysicsProvider.PhysicsTarget.Rotation;
            OnRotationChanged?.Invoke(CurrentRotation);
        }

        public void Tick()
        {
            PositionChanged();
            RotationChanged();
        }
    }
}