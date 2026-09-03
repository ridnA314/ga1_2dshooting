using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float Health = 100f;
    public float MoveSpeedScalar;

    private void Update()
    {
        Move();
    }

    public abstract void Move();

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public bool IsDead()
    {
        if (Health <= 0)
        {
            return true;
        }

        return false;
    }
}