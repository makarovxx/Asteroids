using System;
using System.Collections.Generic;
using Project.Scripts.Gameplay.Entities;
using Project.Scripts.Signals;
using Zenject;

namespace Project.Scripts.Gameplay.Utilities.ScoreSystem
{
    public sealed class ScoreModel : IInitializable, IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;

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


        public void Initialize()
        {
            _signalBus.Subscribe<WeaponHitEnemy>(HandleEnemyDestroy);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<WeaponHitEnemy>(HandleEnemyDestroy);
        }

        private void HandleEnemyDestroy(WeaponHitEnemy signal)
        {
            _rewardsDict.TryGetValue(signal.EnemyDestroyed.EntityType, out var rewards);
            NotifyScoreChanged(rewards);
        }

        private void NotifyScoreChanged(int score)
        {
            CurrentScore += score;
            OnScoreChanged?.Invoke(CurrentScore);
            
            if(CurrentScore >= ScoreRecord)
                UpdateScoreRecord(CurrentScore);
        }

        private void UpdateScoreRecord(int score)
        {
            ScoreRecord = score;
            OnMaxScoreChanged?.Invoke(ScoreRecord);
        }
    }
}