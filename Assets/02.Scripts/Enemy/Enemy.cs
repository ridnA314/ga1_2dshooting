using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] protected float _moveSpeedScalar;

    private void Update()
    {
        Move();
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
}