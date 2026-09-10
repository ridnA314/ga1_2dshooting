using UnityEngine;

public class MoveSpeedItem : Item
{
    [SerializeField]
    private float _moveSpeedBonus = 5f;

    protected override void GiveEffect()
    {
        /*
        if (_playerTransform.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.GrowUpMoveSpeed(_moveSpeedBonus);
            Debug.Log($"player move speed: {playerMove.SpeedScalar}");
        }
        */
    }
}