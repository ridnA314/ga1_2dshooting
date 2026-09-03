using UnityEngine;

public class FollowEnemy : Enemy
{
    public PlayerMove Player;
    private Vector2 _targetDirection;

    private void Start()
    {
        _targetDirection = Player.transform.position - transform.position;
    }

    public override void Move()
    {
        Vector2 distance = _targetDirection * MoveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}