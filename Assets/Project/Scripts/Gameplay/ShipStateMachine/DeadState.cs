using Project.Scripts.Gameplay.Entities.Ship;
using Project.Scripts.InputManageSystem;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.ShipStateMachine
{
    public sealed class DeadState : IState, IExitableState
    {
        private readonly ShipProvider _shipProvider;
        private readonly InputManager _input;
        private Collider2D _shipCollider;
        
        [Inject]
        public DeadState(ShipProvider shipProvider, InputManager input)
        {
            _shipProvider = shipProvider;
            _input = input;
        }

        public void Enter()
        {
            _shipProvider.Physics.SetControlability(false);
            _shipProvider.Ship.TryGetComponent(out _shipCollider);
            _shipCollider.enabled = false;
            _input.SetWeaponInputEnabled(false);
            _shipProvider.Ship.gameObject.SetActive(false);
        }

        public void Exit()
        {
            _shipProvider.Physics.SetControlability(true);
            _shipProvider.Ship.TryGetComponent(out _shipCollider);
            _shipCollider.enabled = true;
            _input.SetWeaponInputEnabled(true);
            _shipProvider.Ship.gameObject.SetActive(true);
        }
    }
}