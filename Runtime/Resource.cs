using UnityEngine;
using UnityEngine.Events;

namespace EMT
{
    [System.Serializable]
    public class ResourceEvent : UnityEvent<float, float, float> { }

    public class Resource : MonoBehaviour, IResource
    {
        // Properties

        private float _max;
        public float Max
        {
            get { return _max; }
            set { _max = value; }
        }

        private float _value;
        public float Value
        {
            get { return _value; }
            set { val = Mathf.Clamp(value, 0, max); }
        }

        // Events

        public ResourceEvent onUpdate = new ResourceEvent();

        // Methods

        public bool IsEmpty() => Value == 0;

        public bool IsFull() => Value == Max;

        public bool HasAtLeast(float leastValue) => Value >= leastValue;

        public void Fill() => Value = Max;

        public void Deplete() => Value = 0;

        public void Add(float amount)
        {
            Value += amount;
            onUpdate.Invoke(amount, Value, Max);
        }

        // Operators override

        public static FillableResource operator +(FillableResource resource, float amount)
        {
            resource.Value += amount;
            return resource;
        }

        public static FillableResource operator -(FillableResource resource, float amount)
        {
            resource.Value -= amount;
            return resource;
        }
    }
}
