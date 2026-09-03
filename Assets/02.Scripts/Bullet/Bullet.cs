using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _power;
    [SerializeField] public float _moveSpeedScalar;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = Vector2.up;
        Vector2 distance = direction * _moveSpeedScalar * Time.deltaTime;

        transform.Translate(distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_power);
        }
    }
}