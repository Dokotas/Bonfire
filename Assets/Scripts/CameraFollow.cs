using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform _target;

    private void Start()
    {
        _target = GameManager.Player.transform;
    }

    void Update()
    {
        var targetPos = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
    }
}
