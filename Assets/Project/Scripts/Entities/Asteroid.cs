using System;
using Project.Scripts.Core;
using Unity.VisualScripting;

namespace Project.Scripts.Entities
{
    public class Asteroid : IInitializable, IDisposable
    {
        private CustomPhysics _physicComponent;


        private void Initialize()
        {
            
        }

        void IInitializable.Initialize()
        {
            Initialize();
        }

        public void Dispose()
        {
            
        }
    }
}