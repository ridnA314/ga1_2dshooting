using UnityEngine;

public class FollowEnemy : Enemy
{
    private Vector2 _targetDirection;

    public override void Initialize(Transform playerTransform)
    {
        if (playerTransform == null) return;
        _targetDirection = playerTransform.position - transform.position;
        _targetDirection = _targetDirection.normalized;
    }

    public override void Move()
    {
        Vector2 distance = _targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}