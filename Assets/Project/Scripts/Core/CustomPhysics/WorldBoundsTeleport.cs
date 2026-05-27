using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.CustomPhysics
{
    public class WorldBoundsTeleport
    {
        private readonly Camera _camera;
        
        [Inject]
        public WorldBoundsTeleport(Camera camera)
        {
            _camera = camera;
        }

        public void TeleportIfOutOfBounds(EntityPhysicsBase entity)
        {
            Vector2 viewportPosition = _camera.WorldToViewportPoint(entity.Position);

            if (viewportPosition.x > 1f)
            {
                viewportPosition.x = 0f;
            }
            else if (viewportPosition.x < 0f)
            {
                viewportPosition.x = 1f;
            }

            if (viewportPosition.y > 1f)
            {
                viewportPosition.y = 0f;
            }
            else if (viewportPosition.y < 0f)
            {
                viewportPosition.y = 1f;
            }

            entity.Position = _camera.ViewportToWorldPoint(viewportPosition);
        }
    }
}