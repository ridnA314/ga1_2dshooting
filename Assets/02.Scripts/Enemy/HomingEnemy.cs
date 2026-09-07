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

        Vector3 targetDirection = _playerTransform.position - transform.position;
        targetDirection = targetDirection.normalized;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeedScalar * Time.deltaTime);

        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}