using System;
using UnityEngine;

namespace WizardGame.Stats
{
    [Serializable]
    public class AddModifier : StatModifier
    {
        public override float Calculate(float baseValue)
        {
            return ModType == ModifierType.Bonus
                ? baseValue + Value
                : baseValue - Value;
        }
    }
}
