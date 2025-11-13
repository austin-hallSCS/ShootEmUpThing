using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Core
{
    /// <summary>
    /// Intermediate class for any stat block that can be modified by the player's core abilites.
    /// </summary>
    public abstract class PlayerModifiableStats : BaseStats
    {
        protected readonly PlayerAbilities ownerAbilities;

        public PlayerModifiableStats(PlayerAbilities abilities)
        {
            if(abilities == null)
            {
                Debug.LogError("PlayerModifiableStats created without abilities!");
            }

            ownerAbilities = abilities;

            ownerAbilities.OnAbilityModifiersUpdated += ApplyAbilityModifiers;
        }

        public virtual void ApplyAbilityModifiers()
        {
            if (ownerAbilities == null) return;

            // Rest all stats to their base value
            foreach(var stat in runtimeStats.Values)
            {
                stat.Init();
            }

            // Apply all modifiers from abilities
            foreach (var mod in ownerAbilities.AllModifiers)
            {
                ApplyModifierToStat(mod);
            }
        }
    }
}
