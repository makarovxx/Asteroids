using System;
using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Gameplay.ShipStateMachine
{
    public class ShipStateMachineBehaviour : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly ShipStateMachine _shipStateMachine;
        
        [Inject]
        public ShipStateMachineBehaviour(SignalBus signalBus, ShipStateMachine shipStateMachine)
        {
            _signalBus = signalBus;
            _shipStateMachine = shipStateMachine;
        }

        public void Initialize()
        {
            _shipStateMachine.SwitchState<VulnerableState>();
            _signalBus.Subscribe<ShipDamageSignal>(HandleShipDamage);
            _signalBus.Subscribe<InvulnerableEndedSignal>(HandleInvulnerableEnded);
            _signalBus.Subscribe<GameOverSignal>(HandleShipDeath);
        }

        private void HandleRestartGame()
        {
            _shipStateMachine.SwitchState<VulnerableState>();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ShipDamageSignal>(HandleShipDamage);
            _signalBus.Unsubscribe<InvulnerableEndedSignal>(HandleInvulnerableEnded);
            _signalBus.Unsubscribe<GameOverSignal>(HandleShipDeath);
        }

        private void HandleInvulnerableEnded()
        {
            _shipStateMachine.SwitchState<VulnerableState>();
        }

        private void HandleShipDeath()
        {
            _shipStateMachine.SwitchState<DeadState>();
        }

        private void HandleShipDamage()
        {
            _shipStateMachine.SwitchState<InvulnerableState>();
        }
    }
}