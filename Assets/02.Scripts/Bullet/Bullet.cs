using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Power;
    public float MoveSpeedScalar;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = Vector2.up;
        Vector2 distance = direction * MoveSpeedScalar * Time.deltaTime;

        transform.Translate(distance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Power);
        }
    }
}