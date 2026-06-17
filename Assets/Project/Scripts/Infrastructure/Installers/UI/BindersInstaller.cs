using MVVM;
using Project.Scripts.UI.Binders;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers.UI
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
        }
    }
}