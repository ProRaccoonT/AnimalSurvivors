using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
    }

    private void Start()
    {
        if(target == null)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        //Vector3 dir = transform.position - target.position;
        //dir = dir.normalized;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
    }
}