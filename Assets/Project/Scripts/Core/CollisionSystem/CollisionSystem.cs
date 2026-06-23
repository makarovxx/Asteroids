using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Entities;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CollisionSystem
{
    public sealed class CollisionSystem
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


        public bool CanCollide(ICollidable a, ICollidable b)
        {
            return a.CollisionPriority > b.CollisionPriority;
        }

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

            Vector2 newVelocityA = _calculator.CalculateBounce(normalA, physA.Velocity.magnitude);

            Vector2 newVelocityB = _calculator.CalculateBounce(normalB, physB.Velocity.magnitude);
            
            physA.SetVelocity(newVelocityA);
            physB.SetVelocity(newVelocityB);

            bool aIsShip = IsShip(a.EntityType);

            if (aIsShip)
                _signalBus.Fire(new ShipCollisionEnemy(b));
            else
                _signalBus.Fire(new ShipCollisionEnemy(a));
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

            _signalBus.Fire(new WeaponHitEnemy(enemy, weaponHitBy));
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