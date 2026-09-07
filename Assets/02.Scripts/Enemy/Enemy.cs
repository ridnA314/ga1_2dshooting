using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Animator _animator;
    static readonly int ANIM_PARAM = Animator.StringToHash("Hit");

    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    protected float _moveSpeedScalar;

    [SerializeField]
    private float _power = 10f;

    private Item _powerItemPrefab;
    private Item _healthItemPrefab;
    private Item _attackSpeedItemPrefab;
    private Item _moveSpeedItemPrefab;

    protected Transform _playerTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    public abstract void Initialize(Transform playerTransform);

    public void SetItems(Item powerItem, Item healthItem, Item attackSpeedItem, Item moveSpeedItem)
    {
        _powerItemPrefab = powerItem;
        _healthItemPrefab = healthItem;
        _attackSpeedItemPrefab = attackSpeedItem;
        _moveSpeedItemPrefab = moveSpeedItem;
    }

    public abstract void Move();

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger(ANIM_PARAM);
        _health -= damage;
        if (_health <= 0)
        {
            DropItem();
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

            Destroy(gameObject);
        }
    }

    private void DropItem()
    {
        if (_playerTransform == null) return;

        int probability = UnityEngine.Random.Range(0, 100);
        if (probability > 40) return;

        Item item;
        if (probability <= 10)
        {
            item = _powerItemPrefab;
        }
        else if (probability <= 20)
        {
            item = _healthItemPrefab;
        }
        else if (probability <= 30)
        {
            item = _moveSpeedItemPrefab;
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