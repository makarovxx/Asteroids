using Project.Scripts.Gameplay.Utilities.Health;
using Project.Scripts.Gameplay.Utilities.ScoreSystem;
using Project.Scripts.Gameplay.Utilities.ShipParameters;
using Project.Scripts.UI.ViewModels;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers.UI
{
    public class ViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShipHealth>().AsSingle();
            // Container.BindInterfacesAndSelfTo<HealthViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShipBodyParameters>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipParametersViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LaserParametersViewModel>().AsSingle().NonLazy();
        }
    }
}