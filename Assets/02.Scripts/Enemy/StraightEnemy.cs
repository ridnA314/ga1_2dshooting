using UnityEngine;

public class StraitEnemy : Enemy
{
    public override void Move()
    {
        Vector2 direction = Vector2.down;
        Vector2 distance = direction * MoveSpeedScalar * Time.deltaTime;

        transform.Translate(distance);
    }
}