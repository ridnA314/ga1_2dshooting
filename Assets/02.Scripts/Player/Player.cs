using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _health;

    public float Health => _health;

    [SerializeField]
    private float _maxHealth = 100f;

    [SerializeField]
    private GameObject _deathEffectPrefab;

    private Animator _animator;
    static readonly int ANIM_PARAM_TRANPAR = Animator.StringToHash("isTransparency");
    static readonly int ANIM_PARAM_HIT = Animator.StringToHash("Hit");

    private AudioSource _damgedAudioSource;

    private float _transparencyTimer;
    private bool _isTransparency;
    public bool IsTransparency => _isTransparency;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damgedAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _health = _maxHealth;
        _transparencyTimer = 0f;
        _isTransparency = false;
    }

    private void Update()
    {
        _transparencyTimer -= Time.deltaTime;
        if (_transparencyTimer <= 0f && _isTransparency)
        {
            UnTransparency();
        }
    }

    public void TakeDamage(float amount)
    {
        _animator.SetTrigger(ANIM_PARAM_HIT);
        _damgedAudioSource.Play();
        _health -= amount;
        if (_health <= 0f)
        {
            SpawnDeathEffect();
            Destroy(gameObject);
        }
    }

    public void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }

    public void GrowUpHealth(float amount)
    {
        _health += amount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void Transparency(float time)
    {
        _isTransparency = true;
        _animator.SetBool(ANIM_PARAM_TRANPAR, true);
        _transparencyTimer = time;
    }

    private void UnTransparency()
    {
        _isTransparency = false;
        _animator.SetBool(ANIM_PARAM_TRANPAR, false);
    }
}