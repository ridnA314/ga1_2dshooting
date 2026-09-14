using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeInitialDataSO", menuName = "Scriptable Objects/UpgradeInitialDataSO")]
public class UpgradeInitialDataSO : ScriptableObject
{
    public string Name;
    public float DefaultValue;
    public float IncreaseValue;
    public float DefaultCost;
    public float IncreaseCost;
}