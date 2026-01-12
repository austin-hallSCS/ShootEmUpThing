using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace WizardGame.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private StatType statType;

        [Tooltip("Does this value need to be rounded down to the nearest whole number?")]
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
                    currentValue = Mathf.Floor(newValue);
                }
                else
                {
                    currentValue = newValue;
                }
            }
        }

        public Stat(StatType statType, bool isRounded, bool increaseIsPositive, bool isIgnored, float cap, bool isCapChangeable, float minValue, float baseValue)
        {
            this.statType = statType;
            this.isRounded = isRounded;
            this.increaseIsPositive = increaseIsPositive;
            this.isIgnored = isIgnored;
            this.cap = cap;
            this.isCapChangeable = isCapChangeable;
            this.minValue = minValue;
            this.baseValue = baseValue;

            CurrentValue = this.baseValue;
        }

        // Clone from another Stat
        public Stat(Stat other)
        {
            statType = other.StatType;
            isRounded = other.isRounded;
            isIgnored = other.IsIgnored;
            cap = other.Cap;
            isCapChangeable = other.isCapChangeable;
            minValue = other.MinValue;
            baseValue = other.BaseValue;
            CurrentValue = baseValue;
        }

        public void Init() => CurrentValue = baseValue;

        // Load the rules for a StatType, so that they don't have to be set in the inspector every time
        public void LoadRules(StatType type)
        {
            SetStatType(type);

            // Fetch rules that match the StatType
            var rules = StatRules.Get(type);

            // Apply rules
            this.isRounded = rules.IsRounded;
            this.increaseIsPositive = rules.IncreaseIsPositive;
            this.isCapChangeable = rules.IsCapChangeable;

            // Overwrite minValue if it is less than DefaultMin (Minimum can go above, but never below the default)
            if (this.minValue < rules.DefaultMin) this.minValue = rules.DefaultMin;

            // Only overwrite cap if it is uninitialized
            if (this.cap == 0) this.cap = rules.DefaultCap;
        }

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
            if (isIgnored) return;
            CurrentValue = GetModifiedValue(mod);
        }

        public float GetModifiedValue(StatModifier mod)
        {
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
