using UnityEngine;

namespace WizardGame.Stats
{
    public enum ModifierType { Bonus, Penalty }

    [System.Serializable]
    public abstract class StatModifier
    {
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public ModifierType ModType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public void SetStatType(StatType newType) => StatType = newType;
        public void SetModType(ModifierType newType) => ModType = newType;
        public void SetValue(float newValue) => Value = newValue;


        public abstract float Calculate(float baseValue);

        public void Reset()
        {
            Value = 0f;
        }
    }
}
