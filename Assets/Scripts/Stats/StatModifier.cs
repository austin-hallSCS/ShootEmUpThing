using UnityEngine;

namespace WizardGame.Stats
{
    public enum ModifierType { Increase, Decrease }
    public enum ValueType { Flat, Percent }

    [System.Serializable]
    public class StatModifier
    {
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public ModifierType ModType { get; private set;}
        [field: SerializeField] public ValueType ValueType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public void SetStatType(StatType newType) => StatType = newType;
        public void SetModType(ModifierType newType) => ModType = newType;
        public void SetValueType(ValueType newType) => ValueType = newType;
        public void SetValue(float newValue) => Value = newValue;

        public StatModifier(StatType statType)
        {
            this.StatType = statType;
        }

        public float ApplyTo(float baseValue)
        {
            float delta = ValueType == ValueType.Flat
                ? Value
                : baseValue * (Value / 100f);

            return ModType == ModifierType.Increase
                ? baseValue + delta
                : baseValue - delta;
        }

        public void Reset()
        {
            Value = 0f;
        }
    }
}
