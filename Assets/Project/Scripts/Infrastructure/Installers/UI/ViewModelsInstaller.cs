using Project.Scripts.Gameplay.Utilities.Health;
using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.UI.ViewModels;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers.UI
{
    public class ViewModelsInstaller : MonoInstaller
    {
        [Inject]  [SerializeField] private PlayerData _playerData;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShipHealth>().AsSingle().WithArguments(_playerData.AmountHealth);
            Container.BindInterfacesAndSelfTo<HealthViewModel>().AsSingle().NonLazy();
        }
    }
}