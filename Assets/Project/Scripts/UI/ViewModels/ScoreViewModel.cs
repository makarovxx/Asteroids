using System;
using MVVM;
using Project.Scripts.Gameplay.Utilities.ScoreSystem;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.ViewModels
{
    public sealed class ScoreViewModel : IInitializable, IDisposable
    {
        private const string CurrencyScoreTitle = "Score: ";
        private const string MaxScoreTitle = "Max Score: ";
        [Data("Score")] 
        public readonly ReactiveProperty<string> CurrentScore = new();
        // [Data("MaxScore")]
        public readonly ReactiveProperty<string> MaxScoreRecord = new();

        private readonly ScoreModel _scoreModel;
        
        public ScoreViewModel(ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
        }

        public void Initialize()
        {
            OnScoreChanged(_scoreModel.CurrentScore);
            OnMaxScoreChanged(_scoreModel.CurrentScore);
            _scoreModel.OnScoreChanged += OnScoreChanged;
            _scoreModel.OnMaxScoreChanged += OnMaxScoreChanged;
        }

        public void Dispose()
        {
            _scoreModel.OnScoreChanged -= OnScoreChanged;
            _scoreModel.OnMaxScoreChanged -= OnMaxScoreChanged;
        }

        private void OnMaxScoreChanged(int newRecordScore)
        {
            MaxScoreRecord.Value = MaxScoreTitle + newRecordScore;
        }

        private void OnScoreChanged(int currentScore)
        {
            CurrentScore.Value = CurrencyScoreTitle + currentScore;
        }
    }
}