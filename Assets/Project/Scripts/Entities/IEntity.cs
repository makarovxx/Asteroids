// using Project.Scripts.Core.CustomPhysics;
// using UnityEngine;
//
// namespace Project.Scripts.Entities
// {
//     public interface IEntity
//     {
//         void Init<T>(T typePhysics) where T : EntityPhysicsBase;
//     }
//
//     public class Entity : MonoBehaviour, IEntity
//     {
//         protected EntityPhysicsBase Physics { get; private set; }
//
//         public void Init<T>(T typePhysics) where T : EntityPhysicsBase
//         {
//             Physics = typePhysics;
//         }
//     }
// }