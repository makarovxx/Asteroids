using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Scripts.UI.Views
{
    public class LaserParametersView : MonoBehaviour
    {
        [Data("Charges")]
        [SerializeField] public TMP_Text _chargesAmount;
        [Data("RechargeProgress")]
        [SerializeField] public Image _rechargeProgress;
    }
}