using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Player
{
    [CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Player/PlayerDataSO")]
    public class PlayerDataSO : ScriptableObject
    {
        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat Experience { get; private set; }
        [field: SerializeField] public Stat MovementSpeed { get; private set; }
        [field: SerializeField] public Stat DamageResistance { get; private set; }

        private void OnValidate()
        {
            // Set StatTypes, so that we don't have to in the inspector
            Health?.SetStatType(StatType.Health);
            Experience.SetStatType(StatType.Experience);
            MovementSpeed.SetStatType(StatType.MovementSpeed);
            DamageResistance.SetStatType(StatType.DamageResistance);
        }

    }
}
