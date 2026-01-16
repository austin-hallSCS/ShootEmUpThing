using System;
using UnityEngine;

namespace WizardGame.Stats
{
    /// <summary>
    /// Modifiers that add a flat value. +10, +5, etc.
    /// </summary>
    [Serializable]
    public class AddModifier : StatModifier
    {
        // REQUIRED: Empty constructor for Unity inspector
        public AddModifier() { }

        public AddModifier(StatType type) : base(type) { }

        public override string GenerateDescription()
        {
            string description = $"+{Value} {StatType}";

            return description;
        }

        public override float CalculateMagnitude(float currentStatValue)
        {
            return Mathf.Abs(Value);
        }
    }
}
