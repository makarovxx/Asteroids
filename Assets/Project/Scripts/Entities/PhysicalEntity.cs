using Project.Scripts.Core.CustomPhysics;
using UnityEngine;

namespace Project.Scripts.Entities
{
    public class PhysicalEntity : MonoBehaviour
    {
        protected IPhysics Physics;

        public void Init(IPhysics physics)
        {
            Physics = physics;
        }
    }
}