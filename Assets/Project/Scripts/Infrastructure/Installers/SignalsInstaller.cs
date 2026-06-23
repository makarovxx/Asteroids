using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PauseGameSignal>();
            Container.DeclareSignal<ResumeGameSignal>();
            Container.DeclareSignal<RestartGameSignal>();
            Container.DeclareSignal<GameOverSignal>();
            
            Container.DeclareSignal<ShipCollisionEnemy>();
            Container.DeclareSignal<WeaponHitEnemy>();
            Container.DeclareSignal<InvulnerableEndedSignal>();
            Container.DeclareSignal<ShipDamageSignal>();
        }
    }
}
