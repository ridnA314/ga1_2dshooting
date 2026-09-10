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
        GameObject target = GameObject.FindGameObjectWithTag("Enemy");
        if (target == null) return;

        //2. get direction
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        direction.y = 0;

        //3.속력에 맞게 이동
        transform.position += direction * _speedScalar * Time.deltaTime;
    }
}