using System;
using UnityEngine;

namespace WizardGame.Stats
{
    [Serializable]
    public class MultModifier : StatModifier
    {
        public override float Calculate(float baseValue)
        {
            var percentValue = Value / 100;
            return ModType == ModifierType.Bonus
                ? baseValue * (1 + percentValue)
                : baseValue * (1 - percentValue);
        }
    }
}
