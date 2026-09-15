using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrade : MonoBehaviour
{
    [SerializeField]
    private UpgradeType _type;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private TextMeshProUGUI _valueText;

    [SerializeField]
    private TextMeshProUGUI _scoreCostText;

    private void Start()
    {
        UpgradeManager.Instance.Initialize(_type);
    }

    public void OnClick()
    {
        UpgradeManager.Instance.LevelUp(_type);
    }

    public void Refresh()
    {
        Upgrade upgrade = UpgradeManager.Instance.Upgrades[(int)_type];

        _titleText.text = $"{upgrade.Name} Lv.{upgrade.Level}";
        _valueText.text = $"{upgrade.CurrentValue}->{upgrade.NextValue}";
        _scoreCostText.text = $"{upgrade.Cost:N0}";
    }
}