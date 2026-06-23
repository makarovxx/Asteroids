using System;
using Project.Scripts.Gameplay.Entities.Ship;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Configs.PersistantData
{
    [Serializable]
    public class ShipPersistantData
    {
        public Ship Prefab;
        public Transform Container;
    }
}
