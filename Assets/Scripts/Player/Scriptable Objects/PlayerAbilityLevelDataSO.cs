using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame
{
    [CreateAssetMenu(fileName = "PlayerAbilityLevelDataSO", menuName = "Player/Player Ability Level Data")]
    public class PlayerAbililtyLevelDataSO : ScriptableObject
    {
        [Range(1, 20)]
        public int level = 1;

        [Tooltip("All stat bonuses that apply at this level.")]
        [SerializeField] private List<StatModifier> modifiers = new();
        public IReadOnlyList<StatModifier> Modifiers => modifiers;
    }
}
