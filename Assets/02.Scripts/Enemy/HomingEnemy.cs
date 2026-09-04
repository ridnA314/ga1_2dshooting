using UnityEngine;

public class HomingEnemy : Enemy
{
    public override void Initialize(Transform playerTransform)
    {
        if (playerTransform == null) return;
        _playerTransform = playerTransform;
    }

    public override void Move()
    {
        if (_playerTransform == null) return;

        Vector2 targetDirection = _playerTransform.position - transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}