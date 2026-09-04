using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _power;

    [SerializeField]
    private float _moveSpeedScalar;

    private float _bonusPowerOfPlayer = 0f;

    private void Update()
    {
        Move();
    }

    public void Initialize(float powerBonus)
    {
        _bonusPowerOfPlayer = powerBonus;
    }

    private void Move()
    {
        Vector2 direction = Vector2.up;
        Vector2 distance = direction * _moveSpeedScalar * Time.deltaTime;

        transform.Translate(distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_power + _bonusPowerOfPlayer);
        }

        Destroy(gameObject);
    }
}