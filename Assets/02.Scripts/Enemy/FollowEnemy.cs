using UnityEngine;

public class FollowEnemy : Enemy
{
    private Vector2 _targetDirection;

    private void Start()
    {
        _targetDirection = GameObject.FindWithTag("Player").transform.position - transform.position;
        _targetDirection = _targetDirection.normalized;
    }

    public override void Move()
    {
        Vector2 distance = _targetDirection * MoveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}