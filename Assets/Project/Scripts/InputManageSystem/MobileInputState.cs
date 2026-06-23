using UnityEngine;

namespace Project.Scripts.InputManageSystem
{
    public sealed class MobileInputState
    {
        private bool _hasActivity;
        private bool _bulletPressed;
        private bool _laserPressed;

        public Vector2 Direction { get; private set; }
        public bool IsThrusting { get; private set; }

        public void SetDirection(Vector2 direction)
        {
            Direction = Vector2.ClampMagnitude(direction, 1f);
            _hasActivity = true;
        }

        public void SetThrust(bool isPressed)
        {
            IsThrusting = isPressed;
            _hasActivity = true;
        }

        public void PressBullet()
        {
            _bulletPressed = true;
            _hasActivity = true;
        }

        public void PressLaser()
        {
            _laserPressed = true;
            _hasActivity = true;
        }

        public bool ConsumeBulletPress()
        {
            bool wasPressed = _bulletPressed;
            _bulletPressed = false;
            return wasPressed;
        }

        public bool ConsumeLaserPress()
        {
            bool wasPressed = _laserPressed;
            _laserPressed = false;
            return wasPressed;
        }

        public bool ConsumeActivity()
        {
            bool hadActivity = _hasActivity;
            _hasActivity = false;
            return hadActivity;
        }
    }
}
