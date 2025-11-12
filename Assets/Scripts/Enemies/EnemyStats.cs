using UnityEngine;
using WizardGame.Core;
using WizardGame.Enemy;

namespace WizardGame.Stats
{
    public class EnemyStats : BaseStats
    {
        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat DamageResistance { get; private set; }
        [field: SerializeField] public Stat MovementSpeed { get; private set; }
        [field: SerializeField] public Stat AttackDamage { get; private set; }
        [field: SerializeField] public Stat AttackCooldown { get; private set; }
        [field: SerializeField] public int RewardExperience { get; private set; }

        public EnemyStats(EnemyDataSO baseData)
        {
            var allStats = new[]
            {
                baseData.Health,
                baseData.DamageResistance,
                baseData.MovementSpeed,
                baseData.AttackDamage,
                baseData.AttackCooldown
            };

            InitializeFromSO(allStats);
        }
    }
}
