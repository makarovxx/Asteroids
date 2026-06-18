using MVVM;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Views
{
    public class ScoreView : MonoBehaviour
    {
        [Data("Score")] [SerializeField] public TMP_Text _currentScore;
    }
}