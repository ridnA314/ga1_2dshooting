using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    protected float _moveSpeedScalar;

    [SerializeField]
    private float _power = 10f;

    private Transform _playerTransform;

    private void Update()
    {
        Move();
    }

    public abstract void Initialize(Transform playerTransform);

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
            if (other.TryGetComponent(out PlayerStatus player))
            {
                player.TakeDamage(_power);
            }

            Destroy(gameObject);
        }
    }
}