using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("충돌!");
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("충돌중");
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("충돌 끝!");
    }
}