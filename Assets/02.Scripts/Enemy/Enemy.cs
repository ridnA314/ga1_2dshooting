using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 100f;
    public float MoveSpeedScalar;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = Vector2.down;
        Vector2 distance = direction * MoveSpeedScalar * Time.deltaTime;

        transform.Translate(distance);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public float GetHealth()
    {
        return Health;
    }
}