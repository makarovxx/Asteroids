// using Project.Scripts.Core.CustomPhysics;
// using Project.Scripts.Entities;
// using Project.Scripts.Plugins;
// using UnityEngine;
// using Zenject;
//
// namespace Project.Scripts.Core.Enemy.Factories
// {
//     // public interface IEntityFactory
//     // {
//     //     T Create<T>() where T : Entity;
//     // }
//     //
//     // public interface IEntityFactory<TPhysics> : IEntityFactory where TPhysics : EntityPhysicsBase
//     // {
//     //     new T Create<T>() where T : Entity;
//     // }
//
//     public class EntityFactory
//     {
//         protected readonly Entity EntityPrefab;
//         protected readonly PhysicsSystem PhysicsSystem;
//
//         protected EntityFactory(Entity entityPrefab, PhysicsSystem physicsSystem)
//         {
//             EntityPrefab = entityPrefab;
//             PhysicsSystem = physicsSystem;
//         }
//
//         public T Create<T>() where T : Entity
//         {
//             var entity = Object.Instantiate(EntityPrefab);
//             PhysicsSystem.Register(entity);
//             return entity as T;
//         }
//     }
//
//     public class AsteroidsFactory : EntityFactory
//     {
//         private readonly RotationResolver _rotationResolver;
//
//         protected AsteroidsFactory(Entity entityPrefab, PhysicsSystem physicsSystem, RotationResolver rotationResolver) : base(entityPrefab, physicsSystem)
//         {
//             _rotationResolver = rotationResolver;
//         }
//
//         public T Create<T>()
//         {
//             var entity = Object.Instantiate(EntityPrefab);
//             return entity as T;
//         }
//     }
// }