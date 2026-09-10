using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerAutoMove : MonoBehaviour
{
    private Animator _animator;
    static readonly int ANIM_PARAM = Animator.StringToHash("x");

    [SerializeField]
    private float _speedScalar;

    [SerializeField]
    private float _distanceScoreCoefficient;

    [SerializeField]
    private float _acceleration;

    private float _currentAcceleration;

    private float _cameraStartX;
    private float _cameraEndX;
    private float _cameraStartY;
    private float _cameraHalfY;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        Camera cam = Camera.main;
        Vector2 cameraCenter = cam.transform.position;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        _cameraStartX = cameraCenter.x - halfWidth;
        _cameraEndX = cameraCenter.x + halfWidth;
        _cameraStartY = cameraCenter.y - halfHeight;
        _cameraHalfY = cameraCenter.y;
    }

    private void Update()
    {
        Vector2 direction = GetTargetVector();
        Vector2 speed = direction * _speedScalar;
        speed = Accelate(speed);
        Move(speed);
    }

    private Vector2 GetTargetVector()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 0) return Vector2.zero;

        Array.Sort(enemies, (i, j) => CalculateScore(j).CompareTo(CalculateScore(i)));

        Vector2 direction = enemies[0].transform.position - transform.position;

        _animator.SetInteger(ANIM_PARAM, (int)direction.x);

        return direction;
    }

    private int CalculateScore(GameObject obj)
    {
        if (obj.TryGetComponent(out Enemy enemy))
        {
            return -70;
        }

        int score = (int)((enemy.Health / enemy.MaxHealth) * 100);
        score = 100 - score;

        Vector3 distanceVector = enemy.transform.position - transform.position;
        float distance = distanceVector.magnitude;
        if (distance <= _speedScalar * _distanceScoreCoefficient)
        {
            score -= 20;
        }
        else if (distance >= (_cameraHalfY - _cameraStartY))
        {
            score -= 50;
        }

        return score;
    }

    private Vector2 Accelate(Vector2 speed)
    {
        _currentAcceleration = 1f;
        if (Input.GetKey(KeyCode.E))
        {
            _currentAcceleration = _acceleration;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            _currentAcceleration = 1f / _acceleration;
        }

        Vector2 acceleratedSpeed = speed * _currentAcceleration;
        return acceleratedSpeed;
    }

    private void Move(Vector2 speed)
    {
        Vector2 distance = speed * Time.deltaTime;

        bool isOverStartX = transform.position.x + distance.x <= _cameraStartX;
        bool isOverEndX = transform.position.x + distance.x >= _cameraEndX;
        bool isOverStartY = transform.position.y + distance.y <= _cameraStartY;
        bool isOverEndY = transform.position.y + distance.y >= _cameraHalfY;

        if (isOverStartX)
        {
            distance = new Vector2(_cameraEndX - transform.position.x, 0);
        }
        else if (isOverEndX)
        {
            distance = new Vector2(_cameraStartX - transform.position.x, 0);
        }

        if (!isOverEndY && !isOverStartY)
        {
            transform.Translate(distance);
        }
    }

    public void GrowUpMoveSpeed(float amount)
    {
        _speedScalar += amount;
    }
}