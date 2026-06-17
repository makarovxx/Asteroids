using UnityEngine;

namespace Project.Scripts.Core.CollisionSystem
{
    public sealed class CollisionResponseCalculator
    {
        public Vector2 GetCollisionNormal(Vector2 positionA, Vector2 positionB)
        {
            Vector2 diff = positionA - positionB;

            return diff == Vector2.zero ? Random.insideUnitCircle.normalized : diff.normalized;
        }

        public Vector2 CalculateBounce(Vector2 collisionNormal, float currentSpeed, float minBounceSpeed = 3f)
        {
            float speed = Mathf.Max(currentSpeed, minBounceSpeed);

            return collisionNormal.normalized * speed;
        }
    }
}