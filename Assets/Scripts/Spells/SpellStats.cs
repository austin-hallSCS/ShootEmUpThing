using UnityEditor;
using UnityEngine;
using WizardGame.SpellSystem;

namespace WizardGame.Stats
{
    public class SpellStats
    {
        [field: SerializeField] SpellDataSO Data;

        public Stat Rarity { get; private set; }
        public Stat ProjectileIntervalTime { get; private set; }
        public Stat DamageAmount { get; private set; }
        public Stat AreaAmount { get; private set; }
        public Stat SpeedAmount { get; private set; }
        public Stat CoolDownTime { get; private set; }
        public Stat KnockbackAmount { get; private set; }
        public Stat ProjectileAmount { get; private set; }
        public Stat DurationTime { get; private set; }
        public Stat PierceAmount { get; private set; }

        public int Level { get; private set; }

        public static SpellStats CopyFrom(SpellDataSO spellData)
        {
            return new SpellStats
            {
                Rarity = new Stat(spellData.Rarity),
                ProjectileIntervalTime = new Stat(spellData.ProjectileIntervalTime),

                DamageAmount = new Stat(spellData.DamageAmount),
                AreaAmount = new Stat(spellData.AreaAmount),
                SpeedAmount = new Stat(spellData.SpeedAmount),
                CoolDownTime = new Stat(spellData.CoolDownTime),
                KnockbackAmount = new Stat(spellData.KnockbackAmount),
                ProjectileAmount = new Stat(spellData.ProjectileAmount),
                DurationTime = new Stat(spellData.DurationTime),
                PierceAmount = new Stat(spellData.PierceAmount),
                Level = 1
            };
        }
        
        public void IncreaseLevel()
        {
            Level++;
        }
    }
}