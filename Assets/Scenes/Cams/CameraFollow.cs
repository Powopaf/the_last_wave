using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float CamFollowSpeed = 2100f;
    private Transform _target;
    
    // Update is called once per frame

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 newPos = new Vector3(_target.position.x, _target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, CamFollowSpeed * Time.deltaTime);
    }
}
