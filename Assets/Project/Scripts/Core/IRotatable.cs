using UnityEngine;

namespace Project.Scripts.Core
{
    public interface IRotatable
    {
        void RotateInstantly(float targetAngle);
        
        void RotateSmoothly(float deltaTime, float targetAngle);
    }
    
    public class RotatableObject : IRotatable
    {
        private Transform _transformObj;
        
        public void RotateInstantly(float targetAngle)
        {
            _transformObj.rotation = Quaternion.Euler(0, 0, targetAngle);
        }

        public void RotateSmoothly(float deltaTime, float targetAngle)
        {
            
        }
    }

    public class ShipRotate : IRotatable
    {
        public Vector2 ShipDirection { get; private set; }
        
        public ShipRotate(Vector2 shipDirection)
        {
            ShipDirection = shipDirection;
        }
        
        public void RotateInstantly(float targetAngle)
        {
            throw new System.NotImplementedException();
        }

        public void RotateSmoothly(float deltaTime, float targetAngle)
        {
            throw new System.NotImplementedException();
        }
    }
}