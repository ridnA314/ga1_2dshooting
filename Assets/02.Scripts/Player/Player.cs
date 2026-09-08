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

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
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
}