using Project.Scripts.Core.CollisionSystem;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.Gameplay.Utilities.World;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class PhysicsSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPhysicsSystem();
        }
        
        private void BindPhysicsSystem()
        {
            Container.Bind<CollisionMatrix>().AsSingle();
            Container.Bind<CollisionResponseCalculator>().AsSingle();
            Container.Bind<CollisionSystem>().AsSingle();
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WorldBoundsTeleport>().AsSingle();
            Container.BindInterfacesAndSelfTo<PhysicsSystem>().AsSingle();
            Container.Bind<CameraSpaceMapper>().AsSingle();
        }
    }
}