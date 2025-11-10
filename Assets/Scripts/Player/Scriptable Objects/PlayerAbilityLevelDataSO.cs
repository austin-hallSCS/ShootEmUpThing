using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame
{
    [CreateAssetMenu(fileName = "PlayerAbilityLevelDataSO", menuName = "Player/Player Ability Level Data")]
    public class PlayerLevelDataSO : ScriptableObject
    {
        [Range(1, 10)]
        public int level = 1;

        [Tooltip("All stat changes that apply when reaching this level.")]
        [SerializeField] private List<StatModifier> modifiers = new();
        public IReadOnlyList<StatModifier> Modifiers => modifiers;
    }
}
