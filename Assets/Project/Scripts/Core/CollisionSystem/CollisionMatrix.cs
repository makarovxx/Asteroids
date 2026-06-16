using System.Collections.Generic;
using Project.Scripts.Entities;

namespace Project.Scripts.Core.CollisionSystem
{
    public enum CollisionResult
    {
        Ignore,  // пара не взаимодействует (астероид ↔ UFO и т.д.)
        Bounce,  // рикошет через Vector2.Reflect (корабль ↔ астероиды/UFO)
        Straight // снаряд проходит насквозь и уничтожает (пуля/лазер → враги)
    }

    /// <summary>
    /// Матрица коллизий: словарь нормализованных пар EntityType → реакция.
    /// Нормализация: (A,B) == (B,A) — всегда меньший enum-значение первым.
    ///
    /// Добавление нового взаимодействия: одна строка в конструкторе.
    /// Остальной код не меняется.
    /// </summary>
    public class CollisionMatrix
    {
        private readonly Dictionary<(EntityType, EntityType), CollisionResult> _matrix;

        public CollisionMatrix()
        {
            _matrix = new Dictionary<(EntityType, EntityType), CollisionResult>
            {
                // ── Bounce: корабль отлетает от врагов ───────────────
                { Pair(EntityType.Ship, EntityType.LargeAsteroid),   CollisionResult.Bounce  },
                { Pair(EntityType.Ship, EntityType.MediumAsteroid),  CollisionResult.Bounce  },
                { Pair(EntityType.Ship, EntityType.SmallAsteroid),   CollisionResult.Bounce  },
                { Pair(EntityType.Ship, EntityType.Ufo),             CollisionResult.Bounce  },

                // ── Straight: пуля уничтожает врага ──────────────────
                { Pair(EntityType.Bullet, EntityType.LargeAsteroid),  CollisionResult.Straight },
                { Pair(EntityType.Bullet, EntityType.MediumAsteroid), CollisionResult.Straight },
                { Pair(EntityType.Bullet, EntityType.SmallAsteroid),  CollisionResult.Straight },
                { Pair(EntityType.Bullet, EntityType.Ufo),            CollisionResult.Straight },

                // ── Straight: лазер уничтожает всё ───────────────────
                { Pair(EntityType.Laser, EntityType.LargeAsteroid),   CollisionResult.Straight },
                { Pair(EntityType.Laser, EntityType.MediumAsteroid),  CollisionResult.Straight },
                { Pair(EntityType.Laser, EntityType.SmallAsteroid),   CollisionResult.Straight },
                { Pair(EntityType.Laser, EntityType.Ufo),             CollisionResult.Straight },

                // Все остальные пары → Ignore (возвращается по умолчанию)
            };
        }

        public CollisionResult GetResult(EntityType a, EntityType b)
        {
            return _matrix.GetValueOrDefault(Pair(a, b), CollisionResult.Ignore);
        }

        // Нормализует пару: (A,B) и (B,A) дают одинаковый ключ
        private (EntityType, EntityType) Pair(EntityType a, EntityType b)
            => a <= b ? (a, b) : (b, a);
    }
}
