using UnityEngine;

namespace Project.Scripts.InputManageSystem
{
    public sealed class InputManager
    {
        private readonly InputDetector _detector;
        private bool _weaponInputEnabled = true;

        public InputDeviceType ActiveDevice => _detector.ActiveStrategy.DeviceType;

        public InputManager(InputDetector detector)
        {
            _detector = detector;
        }

        public Vector2 GetRotationDirection() => _detector.ActiveStrategy.GetRotationDirection();

        public bool IsAccelerateInput() => _detector.ActiveStrategy.IsAccelerateInput();

        public bool IsFireBulletLauncher()
        {
            return _weaponInputEnabled && _detector.ActiveStrategy.IsFireBulletLauncher();
        }

        public bool IsLaserFire()
        {
            return _weaponInputEnabled && _detector.ActiveStrategy.IsLaserFire();
        }

        public void SetWeaponInputEnabled(bool enabled)
        {
            _weaponInputEnabled = enabled;
        }
    }
}
