using Project.Scripts.Entities;
using Project.Scripts.Plugins;
using Zenject;

namespace Project.Scripts.Core.Enemy.Factories
{
    public abstract class UnitsFactory
    {
        public abstract Asteroid Create();
    }

    public class AsteroidFactory : UnitsFactory, Plugins.IFactory<Asteroid>
    {
        private readonly DiContainer _container;

        public AsteroidFactory(DiContainer container)
        {
            _container = container;
        }

        public override Asteroid Create()
        {
            return _container.Resolve<Asteroid>();
        }
    }
}
