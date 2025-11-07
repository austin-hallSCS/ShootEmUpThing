using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.SpellSystem
{
    [CreateAssetMenu(fileName = "SpellDataSO", menuName ="Spells/Spell Data")]
    public class SpellDataSO : ScriptableObject
    {
        [field: SerializeField] public string SpellName { get; private set; }
        [field: SerializeField] public Sprite SpellIcon { get; private set; }


        [field: Header("Non-Modifiable Stats")]
        [field: SerializeField] public Stat Rarity { get; private set; }
        [field: SerializeField] public Stat ProjectileIntervalTime { get; private set; }


        [field: Header("Modifiable Stats")]
        [field: SerializeField] public Stat DamageAmount { get; private set; }
        [field: SerializeField] public Stat AreaAmount { get; private set; }
        [field: SerializeField] public Stat SpeedAmount { get; private set; }
        [field: SerializeField] public Stat CoolDownTime { get; private set; }
        [field: SerializeField] public Stat KnockbackAmount { get; private set; }
        [field: SerializeField] public Stat ProjectileAmount { get; private set; }
        [field: SerializeField] public Stat DurationTime { get; private set; }
        [field: SerializeField] public Stat PierceAmount { get; private set; }

        [Header("Level-Up Data")]
        [SerializeField] private List<SpellLevelDataSO> levelData = new();
        public IReadOnlyList<SpellLevelDataSO> LevelData => levelData;

        // Apply stat changes for the given level
        public void ApplyLevelUp(SpellStats spellStats, int newLevel)
        {
            SpellLevelDataSO levelInfo = levelData.Find(l => l.Level == newLevel);
            if (levelInfo == null)
            {
                Debug.LogWarning($"No level data for {SpellName} level {newLevel}");
                return;
            }

            // Max out all values
            if (levelInfo.Level == 10)
            {
                spellStats.DamageAmount.IncreaseToMax();
                spellStats.AreaAmount.IncreaseToMax();
                spellStats.SpeedAmount.IncreaseToMax();
                spellStats.CoolDownTime.DecreaseToMin();
                spellStats.KnockbackAmount.IncreaseToMax();
                spellStats.ProjectileAmount.IncreaseToMax();
                spellStats.DurationTime.IncreaseToMax();

            }

            // Apply changes based on Stat Type
            else
            {
                foreach (var mod in levelInfo.Modifiers)
                {
                    switch (mod.StatType)
                    {
                        case StatType.Damage:
                            spellStats.DamageAmount.Increase(mod.FlatIncrease);
                            spellStats.DamageAmount.PercentIncrease(mod.PercentIncrease);
                            break;
                        case StatType.Area:
                            spellStats.AreaAmount.Increase(mod.FlatIncrease);
                            spellStats.AreaAmount.PercentIncrease(mod.PercentIncrease);
                            break;
                        case StatType.Speed:
                            spellStats.SpeedAmount.Increase(mod.FlatIncrease);
                            spellStats.SpeedAmount.PercentIncrease(mod.PercentIncrease);
                            break;
                        case StatType.Cooldown:
                            spellStats.CoolDownTime.Decrease(mod.FlatIncrease);
                            spellStats.CoolDownTime.PercentDecrease(mod.PercentIncrease);
                            break;
                        case StatType.Knockback:
                            spellStats.KnockbackAmount.Increase(mod.FlatIncrease);
                            spellStats.KnockbackAmount.PercentIncrease(mod.PercentIncrease);
                            break;
                        case StatType.Amount:
                            spellStats.ProjectileAmount.Increase(mod.FlatIncrease);
                            spellStats.ProjectileAmount.PercentIncrease(mod.PercentIncrease);
                            break;
                        case StatType.Duration:
                            spellStats.DurationTime.Increase(mod.FlatIncrease);
                            spellStats.DurationTime.PercentIncrease(mod.PercentIncrease);
                            break;
                    }
                }
            }
        }
    }
}
