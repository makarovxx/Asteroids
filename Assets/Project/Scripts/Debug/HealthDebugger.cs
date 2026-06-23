using System;
using System.Collections.Generic;
using Project.Scripts.Gameplay.Utilities.Health;
using Project.Scripts.Gameplay.Utilities.ScoreSystem;
using Project.Scripts.Gameplay.Utilities.ShipParameters;
using Project.Scripts.Gameplay.Utilities.VFX;
using Project.Scripts.Gameplay.Utilities.Weapons;
using Project.Scripts.Signals;
using Project.Scripts.UI.ViewModels;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Debug
{
    public class HealthDebugger : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject]
        [ShowInInspector,ReadOnly]
        private ShipHealth _shipHealth;
        
        // [Inject] [ShowInInspector,ReadOnly]
        // private HealthViewModel _healthViewModel;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ScoreModel _scoreModel;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ScoreViewModel _scoreViewModel;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ShipBodyParameters _shipBodyParameters;
        
        [Inject] [ShowInInspector,ReadOnly]
        private ShipParametersViewModel _shipParametersViewModel;
        
        [Inject] [ShowInInspector,ReadOnly]
        private LaserController _laserController;
        
        [Inject] [ShowInInspector,ReadOnly]
        private LaserParametersViewModel _laserParametersViewModel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _signalBus.Fire<RestartGameSignal>();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _signalBus.Fire<PauseGameSignal>();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _signalBus.Fire<ResumeGameSignal>();
            }
        }
    }
}