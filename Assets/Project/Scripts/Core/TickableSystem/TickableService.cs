using System;
using System.Collections.Generic;
using Project.Scripts.Gameplay.Utilities.World;
using Project.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.TickableSystem
{
    public sealed class TickableService : IInitializable, IDisposable, ITickable, IFixedTickable
    {
        [Inject] private readonly SignalBus _signalBus;
        private readonly PhysicsSystem _physicsSystem;
        private readonly IReadOnlyList<IBehaviourTickable> _tickableBehaviours; 
        private bool _isTicking = true;

        public TickableService(IEnumerable<IBehaviourTickable> tickableBehaviours, PhysicsSystem physicsSystem)
        {
            _physicsSystem = physicsSystem;
            _tickableBehaviours = new List<IBehaviourTickable>(tickableBehaviours);
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<PauseGameSignal>(DisableTicks);
            _signalBus.Subscribe<ResumeGameSignal>(EnableTicks);
            _signalBus.Subscribe<RestartGameSignal>(EnableTicks);
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<PauseGameSignal>(DisableTicks);
            _signalBus.Unsubscribe<ResumeGameSignal>(EnableTicks);
            _signalBus.Unsubscribe<RestartGameSignal>(EnableTicks);
        }

        void ITickable.Tick()
        {
            if(!_isTicking) return;
            TickBehaviours(Time.deltaTime);
        }

        void IFixedTickable.FixedTick()
        {
            if(!_isTicking) return;
            _physicsSystem.Tick(Time.fixedDeltaTime);
        }

        private void TickBehaviours(float behaviourDeltaTime)
        {
            foreach (var behaviour in _tickableBehaviours)
            {
                behaviour.Tick(behaviourDeltaTime);
            }
        }

        private void EnableTicks() => _isTicking = true;

        private void DisableTicks() => _isTicking = false;
    }
}