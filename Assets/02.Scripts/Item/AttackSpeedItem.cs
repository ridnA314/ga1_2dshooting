using UnityEngine;

public class AttackSpeedItem : Item
{
    [SerializeField]
    private float _attackSpeedBonus = .5f;

    [SerializeField]
    private float _attackSpeedLimit = .2f;

    protected override void GiveEffect(Player player)
    {
        if (player.TryGetComponent(out PlayerFire playerFire))
        {
            playerFire.GrowUpAttackSpeed(_attackSpeedBonus, _attackSpeedLimit);
        }
    }
}