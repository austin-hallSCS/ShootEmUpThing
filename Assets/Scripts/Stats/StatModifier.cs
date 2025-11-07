using UnityEngine;

namespace WizardGame.Stats
{
    [System.Serializable]
    public class StatModifier
    {
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public float FlatIncrease { get; private set; }
        [field: SerializeField] public float PercentIncrease { get; private set; }
    }
}
