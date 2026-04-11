using Project.Scripts.Entities;

namespace Project.Scripts.Core.Enemy.Factories
{
    public abstract class UnitsFactory
    {
        public abstract Asteroid Create();
    }
}