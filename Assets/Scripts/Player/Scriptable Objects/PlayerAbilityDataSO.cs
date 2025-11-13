using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Stats
{
    // Defines the six standard array scores
    public enum StandardArrayValue
    {
        Score_8 = 8,
        Score_10 = 10,
        Score_12 = 12,
        Score_13 = 13,
        Score_14 = 14,
        Score_15 = 15
    }

    [CreateAssetMenu(fileName = "PlayerAbilityDataSO", menuName = "Player/Ability Data")]
    public class PlayerAbilityDataSO : ScriptableObject
    {
        [field: SerializeField] public string SchoolName{ get; private set; }

        [Header("Starting Values (Standard Array)")]
        [field: SerializeField] public StandardArrayValue StrengthStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public StandardArrayValue DexterityStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public StandardArrayValue ConstitutionStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public StandardArrayValue IntelligenceStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public StandardArrayValue WisdomStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public StandardArrayValue CharismaStartValue { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var assignedValues = new HashSet<StandardArrayValue>();

            assignedValues.Add(StrengthStartValue);
            assignedValues.Add(DexterityStartValue);
            assignedValues.Add(ConstitutionStartValue);
            assignedValues.Add(IntelligenceStartValue);
            assignedValues.Add(WisdomStartValue);
            assignedValues.Add(CharismaStartValue);

            // If the HashSet count is less than 6, then one or more values were duplicates, and couldn't be added.
            if (assignedValues.Count < 6)
            {
                Debug.LogWarning($"Duplicate Standard Array value detected on '{this.name}'!" +
                "Please ensure each value (8, 10, 12, 13, 14, 15) is used only once.", this);
            }
        }
#endif
    }
}
