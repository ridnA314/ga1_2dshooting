using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float SpeedScalar;
    
    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        Vector2 direction = Vector2.up;
        Vector2 distance = direction * SpeedScalar * Time.deltaTime;
        
        transform.Translate(distance);
    }
}
