using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayEventOnRepeat : MonoBehaviour
{
    [SerializeField] private bool _startActive = true;
    private bool timerActive = false;
    private float eventTimer = 0;
    public float EventDelay = 10;
    public UnityEvent TimerEvent;

    private void Start()
    {
        if (_startActive)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        if (!timerActive)
        {
            return;
        }

        eventTimer += Time.deltaTime;

        if (eventTimer < EventDelay)
        {
            return;
        }

        TimerEvent.Invoke();
        eventTimer = 0;
    }

    public void StartTimer()
    {
        timerActive = true;
        eventTimer = 0;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}