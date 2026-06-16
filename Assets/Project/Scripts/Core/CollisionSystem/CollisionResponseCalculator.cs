using UnityEngine;

namespace Project.Scripts.Core.CollisionSystem
{
    /// <summary>
    /// Чистая математика рикошета. Только Vector2, никаких Unity-объектов.
    /// Легко тестируется без MonoBehaviour.
    /// </summary>
    public class CollisionResponseCalculator
    {
        /// <summary>
        /// Нормаль столкновения: направление "от B к A" (куда отлетит A).
        /// Защита от нулевого вектора — если сущности в одной точке.
        /// </summary>
        public Vector2 GetCollisionNormal(Vector2 positionA, Vector2 positionB)
        {
            Vector2 diff = positionA - positionB;

            return diff == Vector2.zero
                ? Random.insideUnitCircle.normalized
                : diff.normalized;
        }

        /// <summary>
        /// Новая скорость после отражения.
        ///
        /// Vector2.Reflect(inVelocity, normal):
        ///   — отражает вектор скорости относительно нормали
        ///   — если тело летело прямо в стену, оно отлетит симметрично
        ///   — bounceFactor < 1.0 гасит скорость (0.8 = потеря 20%)
        ///
        /// Если тело стояло на месте — даём минимальный пинок по нормали.
        /// </summary>
        public Vector2 CalculateBounce(Vector2 incomingVelocity, Vector2 normal, float bounceFactor = 0.8f)
        {
            if (incomingVelocity == Vector2.zero)
                return normal.normalized * bounceFactor;

            return Vector2.Reflect(incomingVelocity, normal.normalized) * bounceFactor;
        }
    }
}
