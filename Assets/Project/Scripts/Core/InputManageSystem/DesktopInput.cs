using UnityEngine;

namespace Project.Scripts.Core.InputManageSystem
{
    public class DesktopInput
    {
        public DirectionRotation GetRotationDirection()
        {
            if (Input.GetKey(KeyCode.W)) return DirectionRotation.Up;
            if (Input.GetKey(KeyCode.S)) return DirectionRotation.Down;
            if (Input.GetKey(KeyCode.A)) return DirectionRotation.Left;
            if (Input.GetKey(KeyCode.D)) return DirectionRotation.Right;

            return DirectionRotation.None;
        }

        public bool IsThrusting()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }
}