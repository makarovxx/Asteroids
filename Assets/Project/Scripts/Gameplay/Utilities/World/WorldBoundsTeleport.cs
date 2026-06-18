using Project.Scripts.Core.CustomPhysics;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.World
{
    public sealed class WorldBoundsTeleport
    {
        private readonly CameraSpaceMapper _mapper;

        [Inject]
        public WorldBoundsTeleport(CameraSpaceMapper mapper)
        {
            _mapper = mapper;
        }

        public void TeleportIfOutOfBounds(IPhysics entity)
        {
            Vector2 viewportPosition = _mapper.WorldToViewport(entity.Position);

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

            entity.Position = _mapper.ViewportToWorld(viewportPosition);
        }
    }
}