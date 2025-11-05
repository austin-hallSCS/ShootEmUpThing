using System;
using UnityEngine;

namespace WizardGame.Stats
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField] public bool IsIgnored { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public float BaseValue { get; private set; }
        
        public float CurrentValue
        {
            get => currentValue;
            private set
            {
                // Makes sure currentValue never goes above maximumn or below minimum
                currentValue = Mathf.Clamp(value, MinValue, MaxValue);
            }
        }

        private float currentValue;

        public void Init() => CurrentValue = BaseValue;
        public void Increase(float amount) => CurrentValue += amount;
        public void PercentIncrease(float amount) => CurrentValue *= (1 + (amount / 100));
        public void Decrease(float amount) => CurrentValue -= amount;

    }
}
