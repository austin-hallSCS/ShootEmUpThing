using UnityEngine;
using WizardGame.SpellSystem;

namespace WizardGame.Stats
{
    public class SpellStats
    {
        public float ProjectileIntervalTime { get; private set; }

        public Stat Rarity { get; private set; }
        public Stat DamageAmount { get; private set; }
        public Stat AreaAmount { get; private set; }
        public Stat SpeedAmount { get; private set; }
        public Stat CooldownTime { get; private set; }
        public Stat KnockbackAmount { get; private set; }
        public Stat ProjectileAmount { get; private set; }
        public Stat DurationTime { get; private set; }
        public Stat PierceAmount { get; private set; }

        public int Level { get; private set; }

        private SpellDataSO data;

        public static SpellStats CopyFrom(SpellDataSO spellData)
        {
            return new SpellStats
            {
                data = spellData,

                Level = 1,

                ProjectileIntervalTime = spellData.ProjectileIntervalTime,

                Rarity = new Stat(spellData.Rarity),
                DamageAmount = new Stat(spellData.DamageAmount),
                AreaAmount = new Stat(spellData.AreaAmount),
                SpeedAmount = new Stat(spellData.SpeedAmount),
                CooldownTime = new Stat(spellData.CoolDownTime),
                KnockbackAmount = new Stat(spellData.KnockbackAmount),
                ProjectileAmount = new Stat(spellData.ProjectileAmount),
                DurationTime = new Stat(spellData.DurationTime),
                PierceAmount = new Stat(spellData.PierceAmount)
            };
        }
        
        public void IncreaseLevel()
        {
            Level++;
            Debug.Log($"{data.SpellName} is level {Level}");
        }
    }
}