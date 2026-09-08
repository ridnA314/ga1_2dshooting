using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _timer = 0f;

    [SerializeField]
    private float _lifeTime = 3f;

    [SerializeField]
    private float _moveSpeedScalar = 5f;

    private Vector2 _targetPosition;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
        {
            Destroy(gameObject);
        }

        Move();
    }

    public void Initialize(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void Move()
    {
        if (_targetPosition == null) return;
        Vector2 targetDirection = _targetPosition - (Vector2)transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(enemy.Health);
        }
    }
}