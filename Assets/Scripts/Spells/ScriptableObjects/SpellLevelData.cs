using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    [System.Serializable]
    public class SpellLevelData
    {
        [Header("Level Settings")]
        [field: Range(1, 10)]
        [field: SerializeField] public int Level { get; private set; }

        [TextArea(2, 3)]
        [field: SerializeField] public string Description { get; private set; }

        [Tooltip("All stat changes that apply when reaching this level.")]
        [SerializeField] private List<StatModifier> modifiers = new();
        public IReadOnlyList<StatModifier> Modifiers => modifiers;
    }
}
