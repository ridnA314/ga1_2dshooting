using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField]
    private float _speedScalar;

    private GameObject _target = null;

    private float _cameraStartX;
    private float _cameraEndX;
    private float _cameraStartY;
    private float _cameraHalfY;

    [SerializeField]
    private float _stopTrackingY;

    private void Awake()
    {
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
        if (_target == null || _target.transform.position.y < -_stopTrackingY)
        {
            FindNearestTarget();
        }

        Move();
    }

    private void Move()
    {
        if (_target == null) return;

        //2. get direction
        Vector3 diff = _target.transform.position - transform.position;
        Vector3 direction = diff;

        // y value of distance is lager than 3 -> up move / other than down move 
        if (diff.y >= 3)
        {
            direction.y = 1;
        }
        else
        {
            direction.y = -1;
        }

        direction.Normalize();

        Vector3 distance = direction * _speedScalar * Time.deltaTime;

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
            transform.position += distance;
        }
    }

    private void FindNearestTarget()
    {
        //1.get Target
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length == 0)
        {
            _target = null;
            return;
        }

        _target = targets[0];
        float minDistance = float.MaxValue;
        float distance;
        //1-1. find nearest target
        foreach (GameObject enemy in targets)
        {
            //y position condition
            if (enemy.transform.position.y < -_stopTrackingY)
            {
                continue;
            }

            // calculate distance
            distance = Vector2.Distance(transform.position, _target.transform.position);
            if (minDistance > distance)
            {
                // change target
                minDistance = distance;
                _target = enemy;
            }
        }
    }
}