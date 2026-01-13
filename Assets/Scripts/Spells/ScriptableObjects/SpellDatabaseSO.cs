using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "SpellDataSO", menuName = "Spells/Spell Database")]
    public class SpellDatabaseSO : ScriptableObject
    {
        [Header("Spells")]
        [SerializeField] private List<GameObject> allSpellPrefabs = new List<GameObject>();

        public IReadOnlyList<GameObject> AllSpellPrefabs => allSpellPrefabs;
    }
}
