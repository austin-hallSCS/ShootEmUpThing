using UnityEngine;

namespace WizardGame.Stats
{
    public enum ModifierType { Bonus, Penalty }
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

        // Keep this here for now, but try to keep any stat modification within the stat itself
        // public float ApplyTo(float baseValue)
        // {
        //     float delta = ValueType == ValueType.Flat
        //         ? Value
        //         : baseValue * (Value / 100f);

        //     return ModType == ModifierType.Bonus
        //         ? baseValue + delta
        //         : baseValue - delta;
        // }

        public void Reset()
        {
            Value = 0f;
        }
    }
}
