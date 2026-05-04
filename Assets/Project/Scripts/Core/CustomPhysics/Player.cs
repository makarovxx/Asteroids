using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class Player : MonoBehaviour
    {
        private DesktopInput _input;
        private DynamicPhysics _dynamicPhysics;

        [Inject]
        private void Construct(DesktopInput input, DynamicPhysics dynamicPhysics)
        {
            _input = input;
            _dynamicPhysics = dynamicPhysics;
        }
        
        public void Tick()
        {
            HandleMovement();
            HandleRotation(Time.deltaTime);
        }
        
        private void HandleMovement()
        {
            if (_input.IsThrusting())
                _dynamicPhysics.Accelerate(Time.deltaTime);
            else
                _dynamicPhysics.ApplyDamping(Time.deltaTime);

            _dynamicPhysics.Move(Time.deltaTime);
        }

        private void HandleRotation(float deltaTime)
        {
            DirectionRotation direction = _input.GetRotationDirection();

            _dynamicPhysics.RotateSmoothly(deltaTime ,direction);
        }
    }
}