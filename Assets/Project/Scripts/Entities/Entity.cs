using System;
using Zenject;

namespace Project.Scripts.Entities
{
    public class Entity : IInitializable, IDisposable, ITickable
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}