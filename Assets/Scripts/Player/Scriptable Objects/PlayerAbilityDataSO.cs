using UnityEngine;

namespace WizardGame.Stats
{
    [CreateAssetMenu(fileName = "PlayerAbilityDataSO", menuName = "Player/Ability Data")]
    public class PlayerAbilityDataSO : ScriptableObject
    {
        [Range(1, 20)]
        [field: SerializeField] public int StrengthStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public int DexterityStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public int ConstitutionStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public int IntelligenceStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public int WisdomStartValue { get; private set; }
        [Range(1, 20)]
        [field: SerializeField] public int CharismaStartValue { get; private set; }
    }
}
