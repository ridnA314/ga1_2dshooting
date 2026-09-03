using UnityEngine;

public class HomingEnemy : Enemy
{
    public PlayerMove Player;

    public override void Move()
    {
        Vector2 targetDirection = Player.transform.position - transform.position;
        Vector2 distance = targetDirection * MoveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}