using Project.Scripts.Core.CustomPhysics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Entities.Enemies.Asteroids
{
    public abstract class Asteroid : PhysicalEntity<SolidPhysics>
    {
        public void Launch(Vector2 direction, float speed)
        {
            Physics.Launch(direction, speed);
        }

        public void Stop()
        {
            Physics.Stop();
        }

        public void SetRandomRotation()
        {
            Physics.SetRandomRotation();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 direction =
                Random.insideUnitCircle.normalized;

            Physics.SetVelocity(direction);
        }
    }
}
