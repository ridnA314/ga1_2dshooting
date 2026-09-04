using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private float _timer = 0f;

    [SerializeField]
    private float _waitingTime = 1.2f;

    [SerializeField]
    private float _moveSpeedScalar = 4f;

    private Transform _playerTransform;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _waitingTime)
        {
            Move();
        }
    }

    private void Initialize(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void Move()
    {
        if (_playerTransform == null) return;
        Vector2 targetDirection = _playerTransform.position - transform.position;
        targetDirection = targetDirection.normalized;
        Vector2 distance = targetDirection * _moveSpeedScalar * Time.deltaTime;
        transform.Translate(distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Player player))
            {
                GiveEffect(player);
            }

            Destroy(gameObject);
        }
    }

    protected abstract void GiveEffect(Player player);
}