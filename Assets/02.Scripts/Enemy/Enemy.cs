using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    protected float _moveSpeedScalar;

    [SerializeField]
    private float _power = 10f;

    private Item _powerItemPrefab;
    private Item _healthItemPrefab;
    private Item _attackSpeedItemPrefab;

    protected Transform _playerTransform;

    private void Update()
    {
        Move();
    }

    public abstract void Initialize(Transform playerTransform);

    public void SetItems(Item powerItem, Item healthItem, Item attackSpeedItem)
    {
        _powerItemPrefab = powerItem;
        _healthItemPrefab = healthItem;
        _attackSpeedItemPrefab = attackSpeedItem;
    }

    public abstract void Move();

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Player player))
            {
                player.TakeDamage(_power);
            }

            DropItem();
            Destroy(gameObject);
        }
    }

    private void DropItem()
    {
        if (_playerTransform == null) return;

        int probability = UnityEngine.Random.Range(0, 100);
        if (probability > 30) return;

        Item item;
        if (probability <= 10)
        {
            item = _powerItemPrefab;
        }
        else if (probability <= 20)
        {
            item = _healthItemPrefab;
        }
        else
        {
            item = _attackSpeedItemPrefab;
        }

        item = Instantiate(item);
        item.Initialize(_playerTransform);
        item.transform.position = transform.position;
    }
}