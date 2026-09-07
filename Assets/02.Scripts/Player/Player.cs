using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _health;

    [SerializeField]
    private float _maxHealth = 100f;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
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