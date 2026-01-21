using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "SpellDataSO", menuName = "Spells/Spell Data")]
    public class SpellDataSO : ScriptableObject
    {
        // Identity
        [field: Header("Identity")]
        [field: SerializeField] public string SpellName { get; private set; }

        [field: TextArea(2, 3)]
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite SpellIcon { get; private set; }

        [field: Header("Combat Effects")]
        [field: Tooltip("Effects applied to enemies from this spell.")]
        [field: SerializeField] public SpellEffectPayload Payload { get; private set; }

        // Non-modifiable stats
        [field: Header("Non-Modifiable Stats")]
        [field: SerializeField] public float ProjectileIntervalTime { get; private set; }

        // Modifiable base stats
        [field: Header("Modifiable Stats")]
        [field: SerializeField] public Stat RarityAmount { get; private set; }
        [field: SerializeField] public Stat DamageAmount { get; private set; }
        [field: SerializeField] public Stat AreaAmount { get; private set; }
        [field: SerializeField] public Stat SpeedAmount { get; private set; }
        [field: SerializeField] public Stat CooldownTime { get; private set; }
        [field: SerializeField] public Stat KnockbackAmount { get; private set; }
        [field: SerializeField] public Stat ProjectileAmount { get; private set; }
        [field: SerializeField] public Stat DurationTime { get; private set; }
        [field: SerializeField] public Stat PierceAmount { get; private set; }

        [Header("Level-Up Progression")]
        [SerializeField] private List<SpellLevelData> levelData = new();
        public IReadOnlyList<SpellLevelData> LevelData => levelData;

        private void OnValidate()
        {
            // Load default values and behavior for each StatType
            RarityAmount?.LoadRules(StatType.Rarity);
            DamageAmount?.LoadRules(StatType.Damage);
            AreaAmount?.LoadRules(StatType.Area);
            SpeedAmount?.LoadRules(StatType.Speed);
            CooldownTime?.LoadRules(StatType.Cooldown);
            KnockbackAmount?.LoadRules(StatType.Knockback);
            ProjectileAmount?.LoadRules(StatType.Amount);
            DurationTime?.LoadRules(StatType.Duration);
            PierceAmount?.LoadRules(StatType.Pierce);

            // Force a save in editor (so checkboxes update immediately)
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif

        }

        public SpellLevelData GetLevelData(int currentLevel)
        {
            return levelData.Find(l => l.Level == currentLevel);
        }
    }
}