using UnityEngine;

public class FollowEnemy : Enemy
{
    private Vector2 _targetDirection;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("플레이어를 찾지 못함");
            Destroy(gameObject);
        }

        _targetDirection = player.transform.position - transform.position;
        _targetDirection = _targetDirection.normalized;
    }

    public override void Move()
    {
        Vector2 distance = _targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}