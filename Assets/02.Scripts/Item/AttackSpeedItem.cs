using UnityEngine;

public class AttackSpeedItem : Item
{
    [SerializeField]
    private float _attackSpeedBonus = .2f;

    [SerializeField]
    private float _attackSpeedLimit = 2.2f;

    protected override void GiveEffect()
    {
        if (_playerTransform.gameObject.TryGetComponent(out PlayerFire playerFire))
        {
            playerFire.GrowUpAttackSpeed(_attackSpeedBonus, _attackSpeedLimit);
            Debug.Log($"player attack speed: {playerFire.AttackCoolTime}");
        }
    }
}