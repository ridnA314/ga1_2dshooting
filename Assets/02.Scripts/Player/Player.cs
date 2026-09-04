using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;

    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void GrowHealth(float amount)
    {
        _health += amount;
    }
}