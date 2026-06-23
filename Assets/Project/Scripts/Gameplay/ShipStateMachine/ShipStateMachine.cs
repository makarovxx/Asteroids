using System;
using System.Collections.Generic;
using Project.Scripts.Core.TickableSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.ShipStateMachine
{
    public interface IEnterableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exit();
    }

    public interface ITickableState
    {
        void Tick(float deltaTime);
    }

    public interface IState : IEnterableState
    {
    }

    public sealed class ShipStateMachine : IBehaviourTickable
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;

        [Inject]
        public ShipStateMachine(List<IState> states, SignalBus signalBus)
        {
            _states = new Dictionary<Type, IState>(states.Count);

            foreach (IState state in states)
                _states.Add(state.GetType(), state);
        }

        void IBehaviourTickable.Tick(float deltaTime)
        {
            if (_currentState is ITickableState tickable)
            {
                tickable.Tick(deltaTime);
            }
        }

        public void SwitchState<TState>() where TState : IState
        {
            if (!CanSwitchState<TState>())
            {
                Debug.LogWarning($"Cannot switch to state of type {typeof(TState)}");
                return;
            }

            if (!TryGetNewState<TState>(out var newState))
            {
                Debug.LogError($"[ShipStateMachine] State {typeof(TState).Name} isn't registered.");
                return;
            }

            if (!TryExitPreviousState())
            {
                Debug.LogWarning("Can't Exit Previous State Because State is not IExitable");
            }

            EnterNewState(newState);
        }

        private void EnterNewState(IState nextState)
        {
            _currentState = nextState;
            _currentState.Enter();
        }

        private bool CanSwitchState<TState>() => _currentState is not TState;

        private bool TryGetNewState<TState>(out IState newState) where TState : IState
        {
            return _states.TryGetValue(typeof(TState), out newState);
        }

        private bool TryExitPreviousState()
        {
            if (_currentState is IExitableState exitableState)
            {
                exitableState.Exit();
                _currentState = null;
                return true;
            }

            return false;
        }
    }
}