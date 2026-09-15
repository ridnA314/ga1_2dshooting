using System;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    //기획자가 채우는 속성
    [SerializeField]
    private UpgradeInitialDataSO _initialData;

    public string Name => _initialData.Name;

    // 실행 중에 동적으로 바뀌는 속성

    [SerializeField]
    private int _level;

    public int Level => _level;

    private float _currentValue;
    public float CurrentValue => _currentValue;

    private float _nextValue;
    public float NextValue => _nextValue;

    private int _cost;
    public int Cost => _cost;

    public void LevelUp()
    {
        _level += 1;

        Calculate();
    }

    public void SetLevel(int level)
    {
        _level = level;

        Calculate();
    }

    public void Calculate()
    {
        // ToDo : 공식에 따라 변화
        // value : 기본값 + 레벨 * 증가량 벨류
        // Cost : 기본 점수 * 증가량 점수 ^ 래밸

        _currentValue = _initialData.DefaultValue + _level * _initialData.IncreaseValue;
        _nextValue = _initialData.DefaultValue + (_level + 1) * _initialData.IncreaseValue;
        _cost = (int)(_initialData.DefaultCost + Mathf.Pow(_initialData.IncreaseCost, _level));
    }
}