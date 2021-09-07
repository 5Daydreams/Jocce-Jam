using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.TimeEvents
{
    [RequireComponent(typeof(PlayEventOnRepeat))]
    public class BurstEventDuringTime  : MonoBehaviour
    {
        [SerializeField] private PlayEventOnRepeat _secondaryTimer;
        [SerializeField] private bool startActive = false;
        [SerializeField] private float timeBetweenEvents;
        [SerializeField] private float _burstDuration;
        private bool burstActive = false;
        private float burstTimer = 0;

        private void Awake()
        {
            _secondaryTimer = this.GetComponent<PlayEventOnRepeat>();
            _secondaryTimer.EventDelay = timeBetweenEvents;
            _secondaryTimer.StopTimer();

            if (startActive)
            {
                StartBurst();
            }
        }

        private void Update()
        {
            if (!burstActive)
            {
                return;
            }

            burstTimer += Time.deltaTime;

            if (burstTimer < _burstDuration)
            {
                return;
            }

            _secondaryTimer.StopTimer();
            burstActive = false;
        }

        public void StartBurst()
        {
            this.gameObject.SetActive(true);
            burstActive = true;
            _secondaryTimer.StartTimer();
            burstTimer = 0;
        }
    }
}