using UnityEngine;

public class StraightEnemy : Enemy
{
    public override void Initialize(Transform playerTransform)
    {
    }

    public override void Move()
    {
        Vector2 direction = Vector2.down;
        Vector2 distance = direction * _moveSpeedScalar * Time.deltaTime;

        transform.Translate(distance);
    }
}