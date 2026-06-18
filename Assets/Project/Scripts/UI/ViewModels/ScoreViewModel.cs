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
        private const string ScoreTitle = "Score: ";

        [Data("Score")] 
        public readonly ReactiveProperty<string> CurrentScore = new();

        private readonly ScoreModel _scoreModel;
        
        public ScoreViewModel(ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
        }

        public void Initialize()
        {
            OnScoreChanged(_scoreModel.CurrentScore);
            _scoreModel.OnScoreChanged += OnScoreChanged;
        }

        public void Dispose()
        {
            _scoreModel.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int currentScore)
        {
            CurrentScore.Value = ScoreTitle + currentScore;
        }
    }
}