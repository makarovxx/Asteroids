using MVVM;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Views
{
    public class HealthView : MonoBehaviour
    {
        [Data("1")]
        [SerializeField] public TMP_Text _healthAmount;
    }
}
