using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Core;
using WizardGame.Spells;

namespace WizardGame.Stats
{
    public class SpellStats : PlayerModifiableStats
    {
        public float ProjectileIntervalTime { get; private set; }

        public int Level
        {
            get => level;
            private set
            {
                level = Mathf.Clamp(value, 1, 10);
            }
        }

        public Stat Rarity { get; private set; }
        public Stat DamageAmount { get; private set; }
        public Stat AreaAmount { get; private set; }
        public Stat SpeedAmount { get; private set; }
        public Stat CooldownTime { get; private set; }
        public Stat KnockbackAmount { get; private set; }
        public Stat ProjectileAmount { get; private set; }
        public Stat DurationTime { get; private set; }
        public Stat PierceAmount { get; private set; }

        private int level;

        private SpellDataSO baseData;

        private readonly List<StatModifier> allLevelUpModifiers = new();

        public SpellStats(SpellDataSO baseData, PlayerAbilities abilities) : base(abilities)
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

            ProjectileIntervalTime = baseData.ProjectileIntervalTime;
            Level = 1;

            ApplyAbilityModifiers();
        }

        public void ApplyLevelUp()
        {
            // Do nothing if at max level
            if (Level >= baseData.LevelData.Count) return;

            Level++;
            Debug.Log($"Spell raised to level {Level}");

            SpellLevelData levelInfo = baseData.GetLevelData(Level);
            if (levelInfo == null)
            {
                Debug.LogWarning($"No level data for {baseData.SpellName} level {Level}");
                return;
            }

            foreach (var modifier in levelInfo.Modifiers)
            {
                Debug.Log($"{modifier.StatType} +{modifier.Value}");
            }

            allLevelUpModifiers.AddRange(levelInfo.Modifiers);

            foreach (var modifier in allLevelUpModifiers)
            {
                Debug.Log($"From allLevelUpModifiers: {modifier.StatType} +{modifier.Value}");
            }


            ApplyAbilityModifiers();

            Debug.Log($"Spell level: {Level}");
            foreach (var stat in runtimeStats.Values)
            {
                Debug.Log($"{stat.StatType}: {stat.CurrentValue}");
            }
        }

        public override void ApplyAbilityModifiers()
        {
            foreach (var stat in runtimeStats.Values) stat.Init();

            // Player ability modifiers
            // DEBUG: Removing ability modifiers in order to get clear picture of level up mods
            // foreach (var mod in ownerAbilities.AllModifiers) ApplyModifierToStat(mod);

            // Level-up modifiers
            foreach (var mod in allLevelUpModifiers) ApplyModifierToStat(mod);
        }
    }
}