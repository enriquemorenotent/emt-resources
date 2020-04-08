using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EMT
{
    public class Resource : MonoBehaviour, IResource
    {
        // Attributes

        [SerializeField] private string label;

        private List<IResourceModifier> modifiers = new List<IResourceModifier>();

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

        public void AddModifier(IResourceModifier modifier) => modifiers.Add(modifier);

        public void UpdateOverTime(float frequency, float duration, float amount) =>
            modifiers.Add(new ResourceTimeModifier(frequency, duration, amount));

        #region Operator override

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

        #endregion

        // Unity methods

        void Update()
        {
            if (modifiers.Count == 0) return;

            modifiers.RemoveAll(modifier => modifier.HasExpired());

            float amount = 0f;
            modifiers.ForEach(modifier => amount += modifier.Execute(Time.deltaTime));

            if (amount == 0) return;

            Value -= amount;

            onUpdate.Invoke(amount, Value, Max);
        }
    }
}
