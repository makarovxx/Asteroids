using System;
using Project.Scripts.Entities;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class AsteroidsConfig
    {
        public LargeAsteroid PrefabLargeAsteroid;
        public MediumAsteroid PrefabMediumAsteroid;
        public SmallAsteroid PrefabSmallAsteroid;
    }
}