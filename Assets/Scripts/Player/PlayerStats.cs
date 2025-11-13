using Mono.Cecil;
using UnityEngine;
using WizardGame.Core;
using WizardGame.Player;

namespace WizardGame.Stats
{
    public class PlayerStats : PlayerModifiableStats
    {
        public int Level { get; private set; }

        public Stat MovementSpeed { get; private set; }
        public Stat DamageResistance { get; private set; }
        public Stat Health { get; private set; }
        public Stat Experience { get; private set; }

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

            Level = 1;

            ApplyAbilityModifiers();
        }

        public override void ApplyAbilityModifiers()
        {
            if(ownerAbilities == null) return;

            foreach (var mod in ownerAbilities.Strength.Modifiers)
            {
                
            }
        }
    }
}
