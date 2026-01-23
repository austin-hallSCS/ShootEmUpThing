using System;
using UnityEngine;

namespace WizardGame.Utils
{
    public class Timer
    {
        public event Action OnTimerDone;

        private float startTime;
        private float duration;

        private bool isActive;

        public Timer(float duration)
        {
            this.duration = duration;
        }

        public void StartTimer()
        {
            startTime = Time.time;
            isActive = true;
        }

        public void StopTimer()
        {
            isActive = false;
        }

        public void Tick()
        {
            if (!isActive) return;

            if (Time.time >= startTime + duration)
            {
                OnTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}
