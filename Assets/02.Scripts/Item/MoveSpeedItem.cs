using UnityEngine;

public class MoveSpeedItem : Item
{
    [SerializeField]
    private float _moveSpeedBonus = 5f;

    protected override void GiveEffect(Player player)
    {
        if (player.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.GrowUpMoveSpeed(_moveSpeedBonus);
        }
    }
}