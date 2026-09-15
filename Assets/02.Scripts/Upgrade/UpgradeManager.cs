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

    private const string SaveKey = "Upgrade.{0}.Level";

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
        Load();

        RefreshUI();
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
        // ToDo : 묻지말고 시켜라
        // 골드 매니저에게 돈이 있는지 물어보고 돈이 있다면 차감 후 업그레이드
        Upgrade upgrade = _upgrades[(int)type];

        if (ScoreManager.Instance.Score < upgrade.Cost) return;

        ScoreManager.Instance.SpendScore(upgrade.Cost);

        upgrade.LevelUp();

        Save();

        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (UI_Upgrade uiUpgrade in _uiUpgrades)
        {
            uiUpgrade.Refresh();
        }
    }

    private void Save()
    {
        // Only save meaningful data
        // Only save level
        for (int i = 0; i < _upgrades.Length; i++)
        {
            PlayerPrefs.SetInt(string.Format(SaveKey, i), _upgrades[i].Level);
        }

        PlayerPrefs.Save();
    }

    private void Load()
    {
        for (int i = 0; i < _upgrades.Length; i++)
        {
            int level = PlayerPrefs.GetInt(string.Format(SaveKey, i), 1);
            _upgrades[i].SetLevel(level);
        }
    }
}