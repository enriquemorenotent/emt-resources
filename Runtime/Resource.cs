using UnityEngine;
using UnityEngine.Events;

namespace EMT
{
    [System.Serializable]
    public class ResourceEvent : UnityEvent<float, float, float> { }

    public class Resource : MonoBehaviour, IResource
    {
        // Attributes

        [SerializeField] private string name;

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
            set { _value = Mathf.Clamp(value, 0, Max); }
        }

        // Events

        public ResourceEvent onUpdate = new ResourceEvent();

        // Methods

        public bool IsEmpty() => Value == 0;

        public bool IsFull() => Value == Max;

        public bool HasAtLeast(float leastValue) => Value >= leastValue;

        public float Percentage() => Value / Max;

        public void Fill() => Value = Max;

        public void Deplete() => Value = 0;

        public void Add(float amount)
        {
            Value += amount;
            onUpdate.Invoke(amount, Value, Max);
        }

        // Operators override

        public static Resource operator +(Resource resource, float amount)
        {
            resource.Value += amount;
            return resource;
        }

        public static Resource operator -(Resource resource, float amount)
        {
            resource.Value -= amount;
            return resource;
        }
    }
}
