using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "SpellDataSO", menuName = "Spells/Spell Database")]
    public class SpellDatabaseSO : ScriptableObject
    {
        [Header("Spells")]
        [SerializeField] private List<SpellDataSO> allSpellsData = new List<SpellDataSO>();

        public IReadOnlyList<SpellDataSO> AllSpellsData => allSpellsData;
    }
}
