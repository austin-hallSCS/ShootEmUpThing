using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.SpellSystem
{
    [CreateAssetMenu(menuName = "Spells/SpellLevelDataSO")]
    public class SpellLevelDataSO : ScriptableObject
    {
        [Range(1, 10)]
        public int Level = 1;

        [Tooltip("All stat changes that apply when reaching this level.")]
        public List<StatModifier> Modifiers = new List<StatModifier>();
    }
}
