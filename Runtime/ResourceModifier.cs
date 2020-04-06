using UnityEngine;

namespace EMT
{
    public class ResourceModifier
    {
        public float frequency;
        public float duration;
        public float delta;

        private float timeToNextExecution;
        private float timeToExpiration;
        private int executionsScheduled = 0;
        private bool infinite;

        public ResourceModifier(float _frequency, float _delta, float _duration)
        {
            infinite = false;
            duration = _duration;
            frequency = _frequency;
            delta = _delta;

            timeToNextExecution = frequency;
            timeToExpiration = duration;
        }

        public ResourceModifier(float _frequency, float _delta)
        {
            infinite = true;
            duration = 1;
            frequency = _frequency;
            delta = _delta;

            timeToNextExecution = frequency;
            timeToExpiration = duration;
        }

        public void ApplyDeltaTime(float deltaTime)
        {
            if (!infinite) timeToExpiration -= deltaTime;
            timeToNextExecution -= deltaTime;
        }

        public bool HasExpired() => timeToExpiration < 0;

        public bool IsInfinite() => infinite;

        // Returns the damage scheduled and resets timers
        public float Execute()
        {
            if (timeToNextExecution < 0)
            {
                var timeSinceLastExecution = Mathf.Abs(timeToNextExecution);
                executionsScheduled += (int)(timeSinceLastExecution / frequency) + 1;
                timeToNextExecution = timeSinceLastExecution % frequency;
            }

            var totalDelta = delta * executionsScheduled;
            executionsScheduled = 0;
            return totalDelta;
        }
    }
}