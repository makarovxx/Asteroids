using Project.Scripts.Gameplay.Utilities.Health;
using Project.Scripts.Gameplay.Utilities.ScoreSystem;
using Project.Scripts.Gameplay.Utilities.ShipParameters;
using Project.Scripts.UI.ViewModels;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Debug
{
    public class HealthDebugger : MonoBehaviour
    {
        [Inject]
        [ShowInInspector,ReadOnly]
        private ShipHealth _shipHealth;
        
        [Inject] [ShowInInspector,ReadOnly]
        private HealthViewModel _healthViewModel;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ScoreModel _scoreModel;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ScoreViewModel _scoreViewModel;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ShipParameters _shipParameters;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ShipParametersViewModel _shipParametersViewModel;
        
    }
}