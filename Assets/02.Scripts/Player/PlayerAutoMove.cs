using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField]
    private float _speedScalar;

    private void Update()
    {
        //1.get Target
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length == 0) return;

        GameObject target = targets[0];
        float minDistance = float.MaxValue;
        float distance;
        //1-1. find nearest target
        foreach (GameObject enemy in targets)
        {
            // calculate distance
            distance = Vector2.Distance(transform.position, target.transform.position);
            if (minDistance > distance)
            {
                // change target
                minDistance = distance;
                target = enemy;
            }
        }

        //2. get direction
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        direction.y = 0;

        //3.move by speed
        transform.position += direction * _speedScalar * Time.deltaTime;
    }
}