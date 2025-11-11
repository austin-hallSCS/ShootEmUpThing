using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.SpellSystem
{
    [CreateAssetMenu(fileName = "SpellDataSO", menuName = "Spells/Spell Data")]
    public class SpellDataSO : ScriptableObject
    {
        // Identity
        [field: Header("Identity")]
        [field: SerializeField] public string SpellName { get; private set; }
        [field: SerializeField] public Sprite SpellIcon { get; private set; }

        // Non-modifiable stats
        [field: Header("Non-Modifiable Stats")]
        [field: SerializeField] public float ProjectileIntervalTime { get; private set; }

        // Modifiable base stats
        [field: Header("Modifiable Stats")]
        [field: SerializeField] public Stat Rarity { get; private set; }
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

        private void OnValidate()
        {
            // Set StatTypes, so that we don't have to do it in the inspector.
            DamageAmount?.SetStatType(StatType.Damage);
            AreaAmount?.SetStatType(StatType.Area);
            SpeedAmount?.SetStatType(StatType.Speed);
            CoolDownTime?.SetStatType(StatType.Cooldown);
            KnockbackAmount?.SetStatType(StatType.Knockback);
            ProjectileAmount?.SetStatType(StatType.Amount);
            DurationTime?.SetStatType(StatType.Duration);
            PierceAmount?.SetStatType(StatType.Pierce);
        }

        // Apply stat changes for the given level
        public void ApplyLevelUp(SpellStats spellStats, int newLevel)
        {
            SpellLevelDataSO levelInfo = levelData.Find(l => l.Level == newLevel);
            if (levelInfo == null)
            {
                Debug.LogWarning($"No level data for {SpellName} level {newLevel}");
                return;
            }

            // Apply changes based on Stat Type
            foreach (var mod in levelInfo.Modifiers)
            {
                switch (mod.StatType)
                {
                    case StatType.Damage:
                        spellStats.DamageAmount.ApplyModifier(mod);
                        break;
                    case StatType.Area:
                        spellStats.AreaAmount.ApplyModifier(mod);
                        break;
                    case StatType.Speed:
                        spellStats.SpeedAmount.ApplyModifier(mod);
                        break;
                    case StatType.Cooldown:
                        spellStats.CooldownTime.ApplyModifier(mod);
                        break;
                    case StatType.Knockback:
                        spellStats.KnockbackAmount.ApplyModifier(mod);
                        break;
                    case StatType.Amount:
                        spellStats.ProjectileAmount.ApplyModifier(mod);
                        break;
                    case StatType.Duration:
                        spellStats.DurationTime.ApplyModifier(mod);
                        break;
                }
            }
        }
    }
}