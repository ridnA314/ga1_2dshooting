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

        Rotation();

        Vector2 targetDirection = _playerTransform.position - transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;

        transform.Translate(distance);
    }

    private void Rotation()
    {
        if (_playerTransform == null) return;

        Vector2 targetDirection = _playerTransform.position - transform.position;
        targetDirection = targetDirection.normalized;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 90f;
        Vector3 targetAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        transform.eulerAngles = targetAngle;
    }
}