using System;
using UnityEngine;

namespace WizardGame.Stats
{
    [System.Serializable]
    public class Stat
    {
        [field: SerializeField] public StatType StatType { get; private set; }
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

        // Clone from another Stat
        public Stat(Stat other)
        {
            StatType = other.StatType;
            IsIgnored = other.IsIgnored;
            MaxValue = other.MaxValue;
            MinValue = other.MinValue;
            BaseValue = other.BaseValue;
        }

        public void Init() => CurrentValue = BaseValue;

        public void SetMaxValue(float value) => MaxValue = value;
        public void SetStatType(StatType newType) => StatType = newType;

        // Increase
        public void Increase(float amount) => CurrentValue += amount;
        public void PercentIncrease(float amount) => CurrentValue *= (1 + (amount / 100));
        public void IncreaseToMax() => CurrentValue = MaxValue;

        // Decrease
        public void Decrease(float amount) => CurrentValue -= amount;
        public void PercentDecrease(float amount) => CurrentValue *= (100 - amount) / 100;
        public void DecreaseToMin() => CurrentValue = MinValue;
    }
}
