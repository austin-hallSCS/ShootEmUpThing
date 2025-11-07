using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.SpellSystem
{
    [CreateAssetMenu(menuName ="Spells/Spell Data")]
    public class SpellDataSO : ScriptableObject
    {
        [field: SerializeField] public string SpellName { get; private set; }
        [field: SerializeField] public Sprite SpellIcon { get; private set; }

        [SerializeField] private List<SpellLevelDataSO> levelData = new();

        public IReadOnlyList<SpellLevelDataSO> LevelData => levelData;

        // Apply stat changes for the given level
        public void ApplyLevelUp(SpellStats spellStats, int newLevel)
        {
            SpellLevelDataSO levelInfo = levelData.Find(l => l.Level == newLevel);
            if (levelInfo == null)
            {
                Debug.LogWarning($"No levell data for {SpellName} level {newLevel}");
                return;
            }
            
            // Apply changes based on Stat Type
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
