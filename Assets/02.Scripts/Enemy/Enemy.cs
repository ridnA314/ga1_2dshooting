using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Animator _animator;
    static readonly int ANIM_PARAM = Animator.StringToHash("Hit");

    private AudioSource _damgedAudioSource;

    [SerializeField]
    private EnemyType _type;

    public EnemyType Type => _type;

    [SerializeField]
    private int _baseHealth;

    private int _health;
    public int Health => _health;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth => _maxHealth;

    [SerializeField]
    protected float _moveSpeedScalar;

    [SerializeField]
    private float _power = 10f;

    [SerializeField]
    private int _itemDropProbability = 30;

    private ItemDropDataTableSO _itemDropDataTable;

    [SerializeField]
    private GameObject _deathEffectPrefab;

    protected Transform _playerTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damgedAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        RefreshHealth();
    }

    private void Update()
    {
        Move();
    }

    public void SetHealthBalance(float multiple)
    {
        _maxHealth = (int)(_baseHealth * multiple);
        RefreshHealth();
    }

    public void RefreshHealth()
    {
        _health = _maxHealth;
    }

    public void OnSpawn(Transform playerTransform, ItemDropDataTableSO itemDropDataTable)
    {
        Initialize(playerTransform);
        SetItems(itemDropDataTable);
        _health = _maxHealth;
    }

    public abstract void Initialize(Transform playerTransform);

    public void SetItems(ItemDropDataTableSO itemDropDataTable)
    {
        _itemDropDataTable = itemDropDataTable;
    }

    public abstract void Move();

    public void TakeDamage(float damage)
    {
        _damgedAudioSource.Play();
        _animator.SetTrigger(ANIM_PARAM);
        _health -= (int)damage;
        if (_health <= 0)
        {
            SpawnDeathEffect();
            DropItem();

            ScoreManager.Instance.AddScore(100);

            gameObject.SetActive(false);
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
                gameObject.SetActive(false);
            }
        }
    }

    private void DropItem()
    {
        if (_playerTransform == null) return;

        int probability = Random.Range(0, 100);
        if (probability > _itemDropProbability) return;

        GameObject itemGameObject = null;
        int totalWeight = 0;
        foreach (ItemDropData data in _itemDropDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        foreach (ItemDropData data in _itemDropDataTable.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                itemGameObject = Instantiate(data.ItemPrefab);
                itemGameObject.transform.position = transform.position;
                break;
            }
        }

        if (itemGameObject.TryGetComponent(out Item item))
        {
            item.Initialize(_playerTransform);
        }
    }
}