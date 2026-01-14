using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    /// <summary>
    /// Contains information that spells use for levelling up. Values are set in the Inspector inside a SpellData object.
    /// </summary>
    [System.Serializable]
    public class SpellLevelData
    {
        [Header("Level Settings")]
        [field: Range(2, 10)]
        [field: SerializeField] public int Level { get; private set; }

        [Tooltip("All stat changes that apply when reaching this level.")]
        [SerializeReference] public List<StatModifier> Modifiers = new();
        // [SerializeReference] private List<StatModifier> modifiers = new();
        // public IReadOnlyList<StatModifier> Modifiers => modifiers;
    }
}
