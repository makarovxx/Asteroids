using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Entities;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CollisionSystem
{
    /// <summary>
    /// Центральная система обработки столкновений. Чистый C#, без MonoBehaviour.
    ///
    /// Ответственности:
    ///   1. Получить пару сущностей от CollisionDetector
    ///   2. Запросить реакцию у CollisionMatrix
    ///   3. Применить математику (Bounce/Straight) через Calculator
    ///   4. Отправить сигнал в SignalBus
    ///
    /// Дедупликация: решается НА СТОРОНЕ CollisionDetector через сравнение
    /// GetInstanceID() — только сторона с меньшим ID вызывает этот метод.
    /// CollisionSystem не знает об этом и не хранит состояние кадра.
    /// </summary>
    public class CollisionSystem
    {
        private readonly CollisionMatrix _matrix;
        private readonly CollisionResponseCalculator _calculator;
        private readonly SignalBus _signalBus;

        [Inject]
        public CollisionSystem(
            CollisionMatrix matrix,
            CollisionResponseCalculator calculator,
            SignalBus signalBus)
        {
            _matrix = matrix;
            _calculator = calculator;
            _signalBus = signalBus;
        }

        // ─────────────────────────────────────────────────────────
        //  Точка входа — вызывается только от стороны с меньшим ID
        // ─────────────────────────────────────────────────────────

        public void HandleCollision(Entity ownerA, Entity ownerB)
        {
            CollisionResult result = _matrix.GetResult(ownerA.EntityType, ownerB.EntityType);

            switch (result)
            {
                case CollisionResult.Bounce:
                    ProcessBounce(ownerA, ownerB);
                    break;

                case CollisionResult.Straight:
                    ProcessStraight(ownerA, ownerB);
                    break;

                case CollisionResult.Ignore:
                default:
                    return;
            }
        }

        private void ProcessBounce(Entity a, Entity b)
        {
            if (a.Physics is not IMovingPhysics physA) return;
            if (b.Physics is not IMovingPhysics physB) return;

            Vector2 posA = a.Physics.Position;
            Vector2 posB = b.Physics.Position;

            Vector2 normalA = _calculator.GetCollisionNormal(posA, posB);
            Vector2 normalB = -normalA;

            Vector2 newVelocityA = _calculator.CalculateBounce(physA.Velocity, normalA);
            Vector2 newVelocityB = _calculator.CalculateBounce(physB.Velocity, normalB);

            physA.SetVelocity(newVelocityA);
            physB.SetVelocity(newVelocityB);


            bool aIsShip = IsShip(a.EntityType);

            if (aIsShip)
                _signalBus.Fire(new ShipHitEnemy(b));
            else
                _signalBus.Fire(new ShipHitEnemy(a));
        }

        private void ProcessStraight(Entity a, Entity b)
        {
            bool aIsWeapon = IsWeapon(a.EntityType);

            Entity enemy;
            Entity weaponHitBy;
            if (aIsWeapon)
            {
                enemy = b;
                weaponHitBy = a;
            }
            else
            {
                enemy = a;
                weaponHitBy = b;
            }

            _signalBus.Fire(new EnemyHitByWeaponSignal(enemy, weaponHitBy));
        }

        private bool IsWeapon(EntityType typeEntity)
        {
            return typeEntity is EntityType.Bullet or EntityType.Laser;
        }

        private bool IsShip(EntityType type)
        {
            return type is EntityType.Ship;
        }
    }
}