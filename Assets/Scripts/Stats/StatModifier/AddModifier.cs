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
        public AddModifier(StatType type) : base(type) { }

        public override float CalculateMagnitude(float currentStatValue)
        {
            return Mathf.Abs(Value);
        }
    }
}
