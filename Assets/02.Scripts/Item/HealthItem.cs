using UnityEngine;

public class HealthItem : Item
{
    [SerializeField]
    private float _healthBonus = 5f;

    protected override void GiveEffect()
    {
        if (_playerTransform.gameObject.TryGetComponent(out Player player))
        {
            player.GrowUpHealth(_healthBonus);
        }
    }
}