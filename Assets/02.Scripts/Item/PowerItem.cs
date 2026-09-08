using UnityEngine;

public class PowerItem : Item
{
    [SerializeField]
    private float _powerBonus = 5f;

    protected override void GiveEffect()
    {
        if (_playerTransform.gameObject.TryGetComponent(out PlayerFire playerFire))
        {
            playerFire.GrowUpPower(_powerBonus);
        }
    }
}