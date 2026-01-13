using System;
using UnityEngine;

namespace WizardGame.Stats
{
    /// <summary>
    /// Modifiers that add a percent of the Stat's current value. +10%, +5%, etc.
    /// </summary>
    [Serializable]
    public class MultModifier : StatModifier
    {
        public MultModifier(StatType type) : base(type) { }

        public override float CalculateMagnitude(float currentStatValue)
        {
            return currentStatValue * (Mathf.Abs(Value) / 100f);
        }
    }
}
