using Project.Scripts.Gameplay.Utilities.Health;
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
    }
}