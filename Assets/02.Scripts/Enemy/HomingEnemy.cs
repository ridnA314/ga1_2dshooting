using UnityEngine;

public class HomingEnemy : Enemy
{
    public override void Move()
    {
        Vector2 targetDirection = GameObject.FindWithTag("Player").transform.position - transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}