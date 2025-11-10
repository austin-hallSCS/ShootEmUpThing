using UnityEngine;

namespace WizardGame.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private StatType statType;
        [SerializeField] private bool isIgnored;
        [SerializeField] private float maxValue;
        [SerializeField] private float minValue;
        [SerializeField] private float baseValue;

        private float currentValue;

        public StatType StatType => statType;
        public bool IsIgnored => isIgnored;
        public float MaxValue => maxValue;
        public float MinValue => minValue;
        public float BaseValue => baseValue;
        public float CurrentValue
        {
            get => currentValue;
            private set
            {
                // Makes sure currentValue never goes above maximumn or below minimum
                currentValue = Mathf.Clamp(value, minValue, maxValue);
            }
        }
        

        // Clone from another Stat
        public Stat(Stat other)
        {
            statType = other.StatType;
            isIgnored = other.IsIgnored;
            maxValue = other.MaxValue;
            minValue = other.MinValue;
            baseValue = other.BaseValue;
            currentValue = baseValue;
        }

        public void Init() => CurrentValue = baseValue;

        public void SetMaxValue(float value) => maxValue = value;
        public void SetStatType(StatType newType) => statType = newType;

        // Increase
        public void Increase(float amount) => CurrentValue += amount;
        public void PercentIncrease(float amount) => CurrentValue *= (1 + (amount / 100));
        public void IncreaseToMax() => CurrentValue = maxValue;

        // Decrease
        public void Decrease(float amount) => CurrentValue -= amount;
        public void PercentDecrease(float amount) => CurrentValue *= (100 - amount) / 100;
        public void DecreaseToMin() => CurrentValue = minValue;
    }
}
