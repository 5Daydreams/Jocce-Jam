using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    private float speed;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        this.transform.Rotate(Vector3.forward,Time.deltaTime * speed*Mathf.Rad2Deg);
    }
}
