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

        // public static SpellStats CopyFrom(SpellDataSO spellData)
        // {
        //     return new SpellStats
        //     {
        //         baseData = spellData,

        //         Level = 1,

        //         ProjectileIntervalTime = spellData.ProjectileIntervalTime,

        //         Rarity = new Stat(spellData.Rarity),
        //         DamageAmount = new Stat(spellData.DamageAmount),
        //         AreaAmount = new Stat(spellData.AreaAmount),
        //         SpeedAmount = new Stat(spellData.SpeedAmount),
        //         CooldownTime = new Stat(spellData.CooldownTime),
        //         KnockbackAmount = new Stat(spellData.KnockbackAmount),
        //         ProjectileAmount = new Stat(spellData.ProjectileAmount),
        //         DurationTime = new Stat(spellData.DurationTime),
        //         PierceAmount = new Stat(spellData.PierceAmount)
        //     };
        // }
        
        public void ApplyLevelUp()
        {
            Level++;

            SpellLevelDataSO levelInfo = baseData.GetLevelData(Level);
            if (levelInfo == null)
            {
                Debug.LogWarning($"No level data for {baseData.SpellName} level {Level}");
                return;
            }

            // Apply changes based on Stat Type
            foreach (var mod in levelInfo.Modifiers)
            {
                var stat = GetStat(mod.StatType);
                stat.ApplyModifier(mod);
                // switch (mod.StatType)
                // {
                //     case StatType.Damage:
                //         DamageAmount.ApplyModifier(mod);
                //         break;
                //     case StatType.Area:
                //         AreaAmount.ApplyModifier(mod);
                //         break;
                //     case StatType.Speed:
                //         SpeedAmount.ApplyModifier(mod);
                //         break;
                //     case StatType.Cooldown:
                //         CooldownTime.ApplyModifier(mod);
                //         break;
                //     case StatType.Knockback:
                //         KnockbackAmount.ApplyModifier(mod);
                //         break;
                //     case StatType.Amount:
                //         ProjectileAmount.ApplyModifier(mod);
                //         break;
                //     case StatType.Duration:
                //         DurationTime.ApplyModifier(mod);
                //         break;
                // }
            }
        }
    }
}