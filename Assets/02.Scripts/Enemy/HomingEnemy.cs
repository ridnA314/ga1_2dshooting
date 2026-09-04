using UnityEngine;

public class HomingEnemy : Enemy
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.Log("플레이어를 찾지 못함");
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        Vector2 targetDirection = _player.transform.position - transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }
}