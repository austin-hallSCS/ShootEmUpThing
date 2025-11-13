using Unity.VisualScripting;
using UnityEngine;

namespace WizardGame.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private StatType statType;

        [Tooltip("Does this value need to be rounded to the nearest whole number?")]
        [SerializeField] private bool isRounded;

        [Tooltip("Does upgrading this stat increase the value?")]
        [SerializeField] private bool increaseIsPositive = true;

        [Tooltip("Does the object ignore this Stat?")]
        [SerializeField] private bool isIgnored;

        [Tooltip("The absolute maximum value this stat can ever reach.")]
        [SerializeField] private float cap;

        [Tooltip("Can the 'Cap' value be changed at runtime (e.g. for Max Health)?")]
        [SerializeField] private bool isCapChangeable = true;

        
        [SerializeField] private float minValue;
        [SerializeField] private float baseValue;

        private float currentValue;

        public StatType StatType => statType;
        public bool IncreaseIsPositive => increaseIsPositive;
        public bool IsIgnored => isIgnored;
        public float Cap => cap;
        public float MinValue => minValue;
        public float BaseValue => baseValue;
        public float CurrentValue
        {
            get => currentValue;
            private set
            {
                // Makes sure currentValue never goes above maximumn or below minimum
                float newValue = Mathf.Clamp(value, minValue, cap);
                if (isRounded)
                {
                    currentValue = Mathf.Round(newValue);
                }
                else
                {
                    currentValue = newValue;
                }
            }
        }

        public Stat(StatType statType, bool increaseIsPositive, bool isIgnored, float cap, bool isCapChangeable, float minValue, float baseValue)
        {
            this.statType = statType;
            this.increaseIsPositive = increaseIsPositive;
            this.isIgnored = isIgnored;
            this.cap = cap;
            this.isCapChangeable = isCapChangeable;
            this.minValue = minValue;
            this.baseValue = baseValue;

            currentValue = this.baseValue;
        }

        // Clone from another Stat
        public Stat(Stat other)
        {
            statType = other.StatType;
            isIgnored = other.IsIgnored;
            cap = other.Cap;
            isCapChangeable = other.isCapChangeable;
            minValue = other.MinValue;
            baseValue = other.BaseValue;
            currentValue = baseValue;
        }

        public void Init() => CurrentValue = baseValue;

        public void SetCap(float newValue)
        {
            if (isCapChangeable)
            {
                cap = newValue;
            }
            else
            {
                Debug.LogWarning($"Attemped to change the cap on a fixed-cap stat: {StatType}.");
            }
            
        }
        public void SetCurrentValue(float newValue) => CurrentValue = newValue;
        public void SetStatType(StatType newType) => statType = newType;

        public void ApplyModifier(StatModifier mod)
        {
            CurrentValue = GetModifiedValue(mod);
        }

        public float GetModifiedValue(StatModifier mod)
        {
            // Edit value based on if it is Flat or Percentage mod
            float delta = mod.ValueType == ValueType.Flat
                ? mod.Value
                : CurrentValue * (mod.Value / 100f);

            // Negate value if Increase is not positive
            bool shouldIncrease =
                (mod.ModType == ModifierType.Bonus && IncreaseIsPositive) ||
                (mod.ModType == ModifierType.Penalty && !IncreaseIsPositive);

            return shouldIncrease ? CurrentValue + delta : CurrentValue - delta;
        }

        public void Increase(float amount) => CurrentValue += amount;
        public void Decrease(float amount) => CurrentValue -= amount;
    }
}
