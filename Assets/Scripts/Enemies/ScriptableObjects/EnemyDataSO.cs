using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Enemy
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Enemy/Enemy Data")]
    public class EnemyDataSO : ScriptableObject
    {
        [field: Header("Identity")]
        [field: SerializeField] public string EnemyName { get; private set; }
        [field: SerializeField] public Sprite EnemySprite { get; private set; }

        [field: Header("Base Stats")]
        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat DamageResistance { get; private set; }
        [field: SerializeField] public Stat MovementSpeed { get; private set; }
        [field: SerializeField] public Stat AttackDamage { get; private set; }
        [field: SerializeField] public Stat AttackCooldown { get; private set; }

        [field: Header("Rewards")]
        [field: SerializeField] public int RewardExperience { get; private set; }
        
        private void OnValidate()
        {
            // Set StatTypes, so we don't have to do it in the inspector
            Health?.SetStatType(StatType.Health);
            DamageResistance?.SetStatType(StatType.DamageResistance);
            MovementSpeed?.SetStatType(StatType.MovementSpeed);
            AttackDamage?.SetStatType(StatType.Damage);
            AttackCooldown?.SetStatType(StatType.Cooldown);
        }
    }
}
