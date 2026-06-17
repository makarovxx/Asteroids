using UnityEngine;

namespace Project.Scripts.Core.CustomPhysics
{
    public class SolidPhysics : MovingPhysics
    {
        public SolidPhysics(Transform body, RotationResolver rotationResolver) : base(body, rotationResolver)
        {
        }
    }
}
