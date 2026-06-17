using System;
using Project.Scripts.Gameplay.Entities.Enemies.Asteroids;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Configs.PersistantData
{
    [Serializable]
    public class AsteroidsPersistantData
    {
        public LargeAsteroid PrefabLargeAsteroid;
        public MediumAsteroid PrefabMediumAsteroid;
        public SmallAsteroid PrefabSmallAsteroid;
        public Transform AsteroidsContainer;
    }
}