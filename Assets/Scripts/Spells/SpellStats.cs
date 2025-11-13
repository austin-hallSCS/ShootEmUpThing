using System.Collections.Generic;
using UnityEngine;
using WizardGame.Core;
using WizardGame.Spells;

namespace WizardGame.Stats
{
    public class SpellStats : BaseStats
    {
        public float ProjectileIntervalTime { get; private set; }

        public int Level { get; private set; }

        public Stat Rarity { get; private set; }
        public Stat DamageAmount { get; private set; }
        public Stat AreaAmount { get; private set; }
        public Stat SpeedAmount { get; private set; }
        public Stat CooldownTime { get; private set; }
        public Stat KnockbackAmount { get; private set; }
        public Stat ProjectileAmount { get; private set; }
        public Stat DurationTime { get; private set; }
        public Stat PierceAmount { get; private set; }

        private SpellDataSO baseData;

        public SpellStats(SpellDataSO baseData)
        {
            this.baseData = baseData;

            var allStats = new[]
            {
                baseData.DamageAmount,
                baseData.AreaAmount,
                baseData.SpeedAmount,
                baseData.CooldownTime,
                baseData.KnockbackAmount,
                baseData.ProjectileAmount,
                baseData.DurationTime,
                baseData.PierceAmount
            };

            InitializeFromSO(allStats);
            Level = 1;
        }
        
        public void ApplyLevelUp()
        {
            Level++;

            SpellLevelDataSO levelInfo = baseData.GetLevelData(Level);
            if (levelInfo == null)
            {
                Debug.LogWarning($"No level data for {baseData.SpellName} level {Level}");
                return;
            }

            ApplyModifiersToStats(levelInfo?.Modifiers);
            
            Debug.Log($"Spell level: {Level}");
        }

        public void ApplyModifiersToStats(IReadOnlyList<StatModifier> mods)
        {
            // Apply changes based on Stat Type
            foreach (var mod in mods)
            {
                var stat = GetStat(mod.StatType);
                stat?.ApplyModifier(mod);
            }
        }
    }
}