using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ShipHitEnemy>();
            Container.DeclareSignal<EnemyHitByWeaponSignal>();
        }
    }
}