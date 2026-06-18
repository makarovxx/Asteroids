using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.InputManageSystem
{
    public class DesktopInput : IInputStrategy
    {
        public DirectionRotation GetRotationDirection()
        {
            if (Input.GetKey(KeyCode.W)) return DirectionRotation.Up;
            if (Input.GetKey(KeyCode.S)) return DirectionRotation.Down;
            if (Input.GetKey(KeyCode.A)) return DirectionRotation.Left;
            if (Input.GetKey(KeyCode.D)) return DirectionRotation.Right;

            return DirectionRotation.None;
        }

        public bool IsAccelerateInput()
        {
            return Input.GetKey(KeyCode.Space);
        }

        public bool IsFireBulletLauncher()
        {
            return Input.GetKeyDown(KeyCode.F);
        }

        public bool IsLaserFire()
        {
            return Input.GetKeyUp(KeyCode.L);
        }
    }

    public class InputManager : ITickable
    {
        private readonly IReadOnlyList<IInputStrategy> _strategies;

        public InputManager(IEnumerable<IInputStrategy> strategies)
        {
            _strategies = new List<IInputStrategy>();
        }

        public void Tick()
        {
            
        }
    }

    public interface IInputStrategy
    {
        bool IsAccelerateInput();

        bool IsFireBulletLauncher();

        bool IsLaserFire();
    }
}