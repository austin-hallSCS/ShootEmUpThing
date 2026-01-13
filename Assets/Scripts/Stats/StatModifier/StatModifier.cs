using UnityEditor.EditorTools;
using UnityEngine;

namespace WizardGame.Stats
{
    /// <summary>
    /// Foundation class for all Stat Modifiers.
    /// </summary>
    public enum ModifierType { Bonus, Penalty }

    [System.Serializable]
    public abstract class StatModifier
    {
        [field: Tooltip("Which stat does this modifier apply to?")]
        [field: SerializeField] public StatType StatType { get; private set; }

        [field: Tooltip("Is the change positive or negative?")]
        [field: SerializeField] public ModifierType ModType { get; private set; }

        [field: Tooltip("How much does this modifier add or subtract? Percentages should still be entered as whole numbers (10% should be 10).")]
        [field: SerializeField] public float Value { get; private set; }

        public StatModifier(StatType type)
        {
            StatType = type;
        }

        public void SetStatType(StatType newType) => StatType = newType;
        public void SetModType(ModifierType newType) => ModType = newType;
        public void SetValue(float newValue) => Value = newValue;

        // Modifiers use this to calculate the amount to change the stat, not the final stat calculation itself. Applying the modifier will be handled by the Stat itself.
        // A +10 modifier returns 10, a +10 percent modifier returns 10% of the Stat's current value.
        public abstract float CalculateMagnitude(float baseValue);

        public void Reset()
        {
            Value = 0f;
        }
    }
}
