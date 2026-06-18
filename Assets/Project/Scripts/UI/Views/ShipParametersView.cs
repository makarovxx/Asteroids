using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.UI.Views
{
    public class ShipParametersView : MonoBehaviour
    {
        [Data("Position")] [SerializeField] public TMP_Text _coordinate;
        [Data("Rotation")] [SerializeField] public TMP_Text _rotation;
    }
}