using UnityEngine;
using WizardGame.Core;
using WizardGame.Player;

namespace WizardGame.Stats
{
    public class PlayerStats : PlayerModifiableStats
    {
        public Stat MovementSpeed { get; private set; }
        public Stat DamageResistance { get; private set; }
        public Stat Health { get; private set; }

        public PlayerStats(PlayerDataSO baseData, PlayerAbilities abilities) : base(abilities)
        {
            var allStats = new[]
            {
                baseData.MovementSpeed,
                baseData.DamageResistance,
                baseData.Health,
                baseData.Experience
            };
            InitializeFromSO(allStats);
        }
    }
}
