using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.SpellSystem
{
    [CreateAssetMenu(fileName = "SpellLevelDataSO", menuName = "Spells/SpellLevelDataSO")]
    public class SpellLevelDataSO : ScriptableObject
    {
        [Range(1, 10)]
        public int Level = 1;

        [Tooltip("All stat changes that apply when reaching this level.")]
        [SerializeField] private List<StatModifier> modifiers = new();
        public IReadOnlyList<StatModifier> Modifiers => modifiers;
    }
}
