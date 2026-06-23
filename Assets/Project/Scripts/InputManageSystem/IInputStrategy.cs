using UnityEngine;

namespace Project.Scripts.InputManageSystem
{
    public enum InputDeviceType
    {
        Desktop,
        Mobile
    }

    public interface IInputStrategy
    {
        InputDeviceType DeviceType { get; }
        bool HasActivity { get; }
        Vector2 GetRotationDirection();
        bool IsAccelerateInput();
        bool IsFireBulletLauncher();
        bool IsLaserFire();
    }
}
