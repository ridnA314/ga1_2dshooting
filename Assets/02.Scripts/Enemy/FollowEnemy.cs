using UnityEngine;

public class FollowEnemy : Enemy
{
    private Vector2 _targetDirection;

    public override void Initialize(Transform playerTransform)
    {
        if (playerTransform == null) return;
        _playerTransform = playerTransform;
        _targetDirection = _playerTransform.position - transform.position;
        _targetDirection = _targetDirection.normalized;

        float dx = _targetDirection.x - transform.position.x;
        float dy = _targetDirection.y - transform.position.y;

        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void Move()
    {
        Vector2 distance = _targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}