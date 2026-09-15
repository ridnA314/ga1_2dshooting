using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    private int _bestScore;
    public int BestScore => _bestScore;
    private int _currrentScore = 0;
    public int Score => _currrentScore;

    private const string SaveKey = "BestScore";

    [SerializeField]
    private TextMeshProUGUI _bestScoreTextUI;

    [SerializeField]
    private TextMeshProUGUI _currrentScoreTextUI;

    public void SpendScore(int amount)
    {
        _currrentScore -= amount;
        Refresh();
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _bestScore = PlayerPrefs.GetInt(SaveKey, 0);
        Refresh();
    }

    public void AddScore(int score)
    {
        if (score < 0) return;

        _currrentScore += score;
        if (_currrentScore > _bestScore)
        {
            _bestScore = _currrentScore;
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();
        }

        Refresh();
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore:N0}";
        _currrentScoreTextUI.text = $"Score: {_currrentScore:N0}";
    }
}