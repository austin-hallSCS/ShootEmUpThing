using UnityEngine;

namespace WizardGame.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private StatType statType;
        [SerializeField] private bool increaseIsPositive = true;
        [SerializeField] private bool isIgnored;
        [SerializeField] private float maxValue;
        [SerializeField] private float minValue;
        [SerializeField] private float baseValue;

        private float currentValue;

        public StatType StatType => statType;
        public bool IncreaseIsPositive => increaseIsPositive;
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

        public Stat(StatType statType, bool increaseIsPositive, bool isIgnored, float maxValue, float minValue, float baseValue)
        {
            this.statType = statType;
            this.increaseIsPositive = increaseIsPositive;
            this.isIgnored = isIgnored;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.baseValue = baseValue;

            currentValue = this.baseValue;
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
        public void SetCurrentValue(float newValue) => currentValue = newValue;
        public void SetStatType(StatType newType) => statType = newType;

        public void ApplyModifier(StatModifier mod)
        {
            currentValue = GetModifiedValue(mod);
        }

        public float GetModifiedValue(StatModifier mod)
        {
            float delta = mod.ValueType == ValueType.Flat
                ? mod.Value
                : BaseValue * (mod.Value / 100f);

            bool shouldIncrease =
                (mod.ModType == ModifierType.Increase && IncreaseIsPositive) ||
                (mod.ModType == ModifierType.Decrease && !IncreaseIsPositive);

            return shouldIncrease ? BaseValue + delta : BaseValue - delta;
        }

        // Decrease
        public void Decrease(float amount) => CurrentValue -= amount;
    }
}
