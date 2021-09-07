using System;
using System.Collections;
using System.Collections.Generic;
using _Code;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class ProjectileDeath : MonoBehaviour
{
    [SerializeField] private float lifetime = 1;
    [SerializeField] private float flightSpeed;
    private Vector3 deathDirection;
    private bool start = false;
    private float aliveTime = 0;

    public void StoreDeathDirection(Vector3 direction)
    {   
        deathDirection = direction;
    }

    public void EnableParticleDrift()
    {
        gameObject.transform.SetParent(null);
        this.gameObject.SetActive(true);
        start = true;
    }

    private void FixedUpdate()
    {
        if(!start)
            return;

        transform.position += deathDirection* flightSpeed * Time.deltaTime;
        aliveTime += Time.deltaTime;

        if (aliveTime > lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}
