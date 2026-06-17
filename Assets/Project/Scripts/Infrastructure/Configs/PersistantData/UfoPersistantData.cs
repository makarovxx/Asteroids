using System;
using Project.Scripts.Gameplay.Entities.Enemies.Ufo;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Configs.PersistantData
{
    [Serializable]
    public class UfoPersistantData
    {
        public Ufo Prefab;
        public Transform Container;
    }
}