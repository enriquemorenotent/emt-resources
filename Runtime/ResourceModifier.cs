using UnityEngine;

namespace EMT
{
    // Modifies a resource for an certain time
    // on small regular periods of time
    public class ResourceTimeModifier : IResourceModifier
    {
        public float frequency;
        public float duration;
        public float delta;

        private float timeToNextExecution;
        private float timeToExpiration;
        private bool infinite;

        public ResourceTimeModifier(float _frequency, float _delta, float _duration)
        {
            infinite = false;
            duration = _duration;
            frequency = _frequency;
            delta = _delta;

            timeToNextExecution = frequency;
            timeToExpiration = duration;
        }

        public ResourceTimeModifier(float _frequency, float _delta)
        {
            infinite = true;
            duration = 1;
            frequency = _frequency;
            delta = _delta;

            timeToNextExecution = frequency;
            timeToExpiration = duration;
        }

        public bool HasExpired() => timeToExpiration < 0;

        public bool IsInfinite() => infinite;

        // Returns the damage scheduled and resets timers
        public float Execute(float deltaTime)
        {
            if (!infinite) timeToExpiration -= deltaTime;
            timeToNextExecution -= deltaTime;

            if (timeToNextExecution > 0) return 0;

            var timeSinceLastExecution = Mathf.Abs(timeToNextExecution);
            int executionsScheduled = (int)(timeSinceLastExecution / frequency) + 1;
            timeToNextExecution = timeSinceLastExecution % frequency;

            var totalDelta = delta * executionsScheduled;
            return totalDelta;
        }
    }
}