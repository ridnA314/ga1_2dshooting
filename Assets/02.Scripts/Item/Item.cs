using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private float _timer = 0f;

    [SerializeField]
    private float _waitingTime = 1.2f;

    [SerializeField]
    private float _moveSpeedScalar = 4f;

    protected Transform _playerTransform;

    [SerializeField]
    private GameObject _itemEffectPrefab;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _waitingTime)
        {
            Move();
        }
    }

    public void Initialize(Transform playerTransform)
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
                GiveEffect();
            }

            SpawnItemEffect();
            Destroy(gameObject);
        }
    }

    public void SpawnItemEffect()
    {
        Instantiate(_itemEffectPrefab, _playerTransform.position, Quaternion.identity);
    }

    protected abstract void GiveEffect();
}