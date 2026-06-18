using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.World
{
    public sealed class CameraSpaceMapper
    {
        private readonly Camera _camera;
        
        [Inject]
        public CameraSpaceMapper(Camera camera)
        {
            _camera = camera;
        }

        public Vector2 WorldToViewport(Vector2 worldPosition)
        {
            return _camera.WorldToViewportPoint(worldPosition);
        }

        public Vector2 ViewportToWorld(Vector2 position)
        {
            return _camera.ViewportToWorldPoint(position);
        }
    }
}