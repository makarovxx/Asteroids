using Project.Scripts.Core.CustomPhysics;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core
{
    public class PlayerTest : MonoBehaviour
    {
        private SolidPhysics _solidPhysics;

        [Inject]
        private void Construct(SolidPhysics solidPhysics)
        {
            _solidPhysics = solidPhysics;
        }
    }
}