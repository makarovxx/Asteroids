using Zenject;

namespace Project.Scripts.Gameplay.ShipStateMachine
{
    public sealed class VulnerableState : IState
    {
        private readonly InvulnerabilitySystem _invulnerabilitySystem;
    
        [Inject]
        public VulnerableState(InvulnerabilitySystem invulnerabilitySystem)
        {
            _invulnerabilitySystem = invulnerabilitySystem;
        }

        public void Enter()
        {
            _invulnerabilitySystem.DeactivateInvulnerability();
        }
    }
}