using Project.Scripts.Configs;
using Project.Scripts.Core.CustomPhysics;
using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Installers
{
    public class TestInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        public override void InstallBindings()
        {
            Container.Bind<DesktopInput>().AsSingle();
            Container.Bind<RotationResolver>().AsSingle();
            Container.Bind<DynamicPhysics>().AsSingle().WithArguments(_playerConfig.Body,_playerConfig.Velocity, _playerConfig.Acceleration, _playerConfig.Damping, _playerConfig.RotationSpeed);
        }
    }
}
