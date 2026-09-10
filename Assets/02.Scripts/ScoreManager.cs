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

    public void AddScore(int score)
    {
        if (score < 0) return;

        _currrentScore += score;
        if (_currrentScore > _bestScore)
        {
            _bestScore = _currrentScore;
        }
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _currrentScoreTextUI.text = $"Score: {_currrentScore}";
    }
}