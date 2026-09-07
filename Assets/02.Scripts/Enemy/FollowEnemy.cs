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

        Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
        float timer = 0f;
        while (timer < 1f)
        {
            Quaternion.Lerp(transform.rotation, targetRotation, timer);
            timer += Time.deltaTime;
        }
    }

    public override void Move()
    {
        Vector2 distance = _targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}