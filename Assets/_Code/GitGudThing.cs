using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GitGudThing : MonoBehaviour
{
    public float spinSpeed = 1.0f; 
    public float growthSpeed = 1.0f;
    public float duration = 1.0f;

    private float timer = 0;
    private void Update()
    {
        if (timer >= duration)
        {
            this.gameObject.SetActive(false);
        }
        
        this.gameObject.transform.Rotate(Vector3.forward,Time.deltaTime * spinSpeed);
        this.transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one*(1+0.4f*Mathf.Sin(growthSpeed*Time.time)),growthSpeed*Time.deltaTime);
        timer += Time.deltaTime;
    }

    private void Awake()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        timer = 0;
        this.gameObject.SetActive(true);
        this.transform.localScale = Vector3.one * 0.01f;
        this.transform.rotation = quaternion.identity;
    }
}
