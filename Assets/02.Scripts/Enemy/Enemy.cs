using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Animator _animator;
    static readonly int ANIM_PARAM = Animator.StringToHash("Hit");

    //ToDo: play when Enemy is attacked
    private AudioSource _damgedAudioSource;

    [SerializeField]
    private float _health = 100f;

    public float Health => _health;

    [SerializeField]
    protected float _moveSpeedScalar;

    [SerializeField]
    private float _power = 10f;

    private Item _powerItemPrefab;
    private Item _healthItemPrefab;
    private Item _attackSpeedItemPrefab;
    private Item _moveSpeedItemPrefab;
    private Item _transparencyItemPrefab;

    [SerializeField]
    private GameObject _deathEffectPrefab;

    protected Transform _playerTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damgedAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
    }

    public abstract void Initialize(Transform playerTransform);

    public void SetItems(Item powerItem, Item healthItem, Item attackSpeedItem, Item moveSpeedItem,
        Item transparencyItem)
    {
        _powerItemPrefab = powerItem;
        _healthItemPrefab = healthItem;
        _attackSpeedItemPrefab = attackSpeedItem;
        _moveSpeedItemPrefab = moveSpeedItem;
        _transparencyItemPrefab = transparencyItem;
    }

    public abstract void Move();

    public void TakeDamage(float damage)
    {
        _damgedAudioSource.Play();
        _animator.SetTrigger(ANIM_PARAM);
        _health -= damage;
        if (_health <= 0)
        {
            SpawnDeathEffect();
            DropItem();

            Destroy(gameObject);
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Player player))
            {
                if (player.IsTransparency) return;

                player.TakeDamage(_power);
                Destroy(gameObject);
            }
        }
    }

    private void DropItem()
    {
        if (_playerTransform == null) return;

        int probability = UnityEngine.Random.Range(0, 100);
        if (probability > 40) return;

        Item item;
        if (probability <= 8)
        {
            item = _powerItemPrefab;
        }
        else if (probability <= 16)
        {
            item = _healthItemPrefab;
        }
        else if (probability <= 24)
        {
            item = _moveSpeedItemPrefab;
        }
        else if (probability <= 32)
        {
            item = _attackSpeedItemPrefab;
        }
        else
        {
            item = _transparencyItemPrefab;
        }

        item = Instantiate(item);
        item.Initialize(_playerTransform);
        item.transform.position = transform.position;
    }
}