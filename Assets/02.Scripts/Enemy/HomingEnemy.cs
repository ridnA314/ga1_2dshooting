using UnityEngine;

public class HomingEnemy : Enemy
{
    private GameObject _player;

    private Transform _playerTransform;

    public override void Initialize(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public override void Move()
    {
        Vector2 targetDirection = _playerTransform.position - transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}