using UnityEngine;

namespace Project.Scripts.InputManageSystem
{
    public sealed class InputMobileStrategy : IInputStrategy
    {
        private readonly MobileInputState _state;

        public InputDeviceType DeviceType => InputDeviceType.Mobile;
        public bool HasActivity => _state.ConsumeActivity();

        public InputMobileStrategy(MobileInputState state)
        {
            _state = state;
        }

        public Vector2 GetRotationDirection() => _state.Direction;

        public bool IsAccelerateInput() => _state.IsThrusting;

        public bool IsFireBulletLauncher() => _state.ConsumeBulletPress();

        public bool IsLaserFire() => _state.ConsumeLaserPress();
    }
}
