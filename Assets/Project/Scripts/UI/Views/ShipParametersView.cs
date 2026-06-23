using MVVM;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Views
{
    public class ShipParametersView : MonoBehaviour
    {
        [Data("Health")] [SerializeField] public TMP_Text _healthAmount;
        [Data("Position")] [SerializeField] public TMP_Text _coordinate;
        [Data("Rotation")] [SerializeField] public TMP_Text _rotation;
        [Data("Speed")] [SerializeField] public TMP_Text _speed;
    }
}