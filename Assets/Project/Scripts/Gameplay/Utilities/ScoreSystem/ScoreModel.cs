using System;
using System.Collections.Generic;
using Project.Scripts.Gameplay.Entities;
using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.ScoreSystem
{
    public class ScoreModel : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        private readonly Dictionary<EntityType, int> _rewardsDict = new()
        {
            { EntityType.LargeAsteroid, 10 },
            { EntityType.MediumAsteroid, 20 },
            { EntityType.SmallAsteroid, 30 },
            { EntityType.Ufo, 25 }
        };

        public event Action<int> OnScoreChanged;
        public event Action<int> OnMaxScoreChanged;
        public int CurrentScore { get; private set; }
        public int ScoreRecord { get; private set; }

        [Inject]
        public ScoreModel(SignalBus signalBus)
        {
            CurrentScore = 0;
            _signalBus = signalBus;
        }


        public void Initialize()
        {
            _signalBus.Subscribe<EnemyHitByWeaponSignal>(HandleEnemyDestroy);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyHitByWeaponSignal>(HandleEnemyDestroy);
        }

        private void HandleEnemyDestroy(EnemyHitByWeaponSignal signal)
        {
            _rewardsDict.TryGetValue(signal.EnemyDestroyed.EntityType, out var rewards);
            NotifyScoreChanged(rewards);
        }

        private void NotifyScoreChanged(int score)
        {
            CurrentScore += score;
            OnScoreChanged?.Invoke(CurrentScore);
        }

        private void UpdateScoreRecord(int score)
        {
            ScoreRecord += score;
        }
    }
}