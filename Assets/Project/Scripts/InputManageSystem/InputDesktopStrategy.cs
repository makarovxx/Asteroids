using UnityEngine;

namespace Project.Scripts.InputManageSystem
{
    public sealed class InputDesktopStrategy : IInputStrategy
    {
        private Vector3 _lastMousePosition;

        public InputDeviceType DeviceType => InputDeviceType.Desktop;

        public bool HasActivity
        {
            get
            {
                if (Input.touchCount > 0)
                    return false;

                bool mouseMoved = (Input.mousePosition - _lastMousePosition).sqrMagnitude > 0.25f;
                _lastMousePosition = Input.mousePosition;

                return Input.anyKey
                       || mouseMoved
                       || Input.mouseScrollDelta.sqrMagnitude > 0f;
            }
        }

        public Vector2 GetRotationDirection()
        {
            float horizontal = 0f;
            float vertical = 0f;

            if (Input.GetKey(KeyCode.A)) horizontal -= 1f;
            if (Input.GetKey(KeyCode.D)) horizontal += 1f;
            if (Input.GetKey(KeyCode.S)) vertical -= 1f;
            if (Input.GetKey(KeyCode.W)) vertical += 1f;

            return new Vector2(horizontal, vertical).normalized;
        }

        public bool IsAccelerateInput() => Input.GetKey(KeyCode.Space);

        public bool IsFireBulletLauncher() => Input.GetKeyDown(KeyCode.F);

        public bool IsLaserFire() => Input.GetKeyDown(KeyCode.L);
    }
}
