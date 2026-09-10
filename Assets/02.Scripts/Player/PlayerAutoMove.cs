using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField]
    private float _speedScalar;

    private GameObject _target = null;

    private void Update()
    {
        if (_target == null)
        {
            FindNearestTarget();
        }

        Move();
    }

    private void Move()
    {
        if (_target == null) return;

        //2. get direction
        Vector3 direction = _target.transform.position - transform.position;
        direction.Normalize();
        direction.y = 0;

        //3.move by speed
        transform.position += direction * _speedScalar * Time.deltaTime;
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