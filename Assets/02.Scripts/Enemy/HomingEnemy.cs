using UnityEngine;

public class HomingEnemy : Enemy
{
    [SerializeField]
    private float _rotationSpeedScalar = 10f;

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

        Rotation();

        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }

    private void Rotation()
    {
        float dx = _playerTransform.position.x - transform.position.x;
        float dy = _playerTransform.position.y - transform.position.y;

        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        Vector3 targetAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        //transform.eulerAngles = targetAngle;
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngle, Time.deltaTime * _rotationSpeedScalar);
    }
}