using Project.Scripts.Infrastructure.Configs.SerializableData;
using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Gameplay.ShipStateMachine
{
    public sealed class InvulnerableState : IState, ITickableState
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private readonly InvulnerabilitySystem _invulnerabilitySystem;
        private readonly float _duration;
        
        private float _elapsedTime;

        public InvulnerableState(ShipData shipData, InvulnerabilitySystem invulnerabilitySystem)
        {
            _duration = shipData.InvulnerabilityDuration;
            _invulnerabilitySystem = invulnerabilitySystem;
        }

        public void Enter()
        {
            _elapsedTime = _duration;
            _invulnerabilitySystem.ActivateInvulnerability();
        }

        public void Tick(float deltaTime)
        {
            _elapsedTime -= deltaTime;

            if (_elapsedTime <= 0f)
                _signalBus.Fire<InvulnerableEndedSignal>();
        }
    }
}