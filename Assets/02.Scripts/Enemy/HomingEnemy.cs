using UnityEngine;

public class HomingEnemy : Enemy
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Move()
    {
        Vector2 targetDirection = _player.transform.position - transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}