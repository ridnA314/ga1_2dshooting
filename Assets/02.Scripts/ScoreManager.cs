using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _bestScore;
    private int _currrentScore;

    [SerializeField]
    private TextMeshProUGUI _bestScoreTextUI;

    [SerializeField]
    private TextMeshProUGUI _currrentScoreTextUI;

    private void Update()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _currrentScoreTextUI.text = $"CurrentScore: {_currrentScore}";
    }
}