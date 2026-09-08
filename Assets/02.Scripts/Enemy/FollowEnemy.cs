using UnityEngine;

public class FollowEnemy : Enemy
{
    private Vector2 _targetDirection;

    public override void Initialize(Transform playerTransform)
    {
        if (playerTransform == null) return;
        _playerTransform = playerTransform;

        Rotation();

        _targetDirection = _playerTransform.position - transform.position;
        _targetDirection = _targetDirection.normalized;
    }

    public override void Move()
    {
        Vector2 distance = _targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }

    private void Rotation()
    {
        Vector2 targetDirection = _playerTransform.position - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 90f;
        Vector3 targetAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        transform.eulerAngles = targetAngle;
    }
}