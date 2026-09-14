using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance = null;
    public static UpgradeManager Instance => _instance;

    [SerializeField]
    private Upgrade[] _upgrades;

    public Upgrade[] Upgrades => _upgrades;

    [SerializeField]
    private UI_Upgrade[] _uiUpgrades;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public float Get(UpgradeType type)
    {
        return _upgrades[(int)type].CurrentValue;
    }

    public void Initialize(UpgradeType type)
    {
        _upgrades[(int)type].Calculate();
    }

    public void LevelUp(UpgradeType type)
    {
        Upgrade upgrade = _upgrades[(int)type];

        if (ScoreManager.Instance.Score < upgrade.Cost) return;

        ScoreManager.Instance.SpendScore(upgrade.Cost);

        upgrade.LevelUp();

        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (UI_Upgrade uiUpgrade in _uiUpgrades)
        {
            uiUpgrade.Refresh();
        }
    }
}