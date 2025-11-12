using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Core
{
    public abstract class BaseStats
    {
        protected readonly Dictionary<StatType, Stat> runtimeStats = new();

        // Clones all stats from a ScriptableObject into runtime stats.
        protected void InitializeFromSO(IEnumerable<Stat> baseStats)
        {
            runtimeStats.Clear();

            foreach (var baseStat in baseStats)
            {
                if (baseStat == null) continue;

                Stat cloned = new(baseStat);
                cloned.Init();
                runtimeStats[baseStat.StatType] = cloned;
            }
        }

        // Gets the runtime Stat instance by type
        public Stat GetStat(StatType type)
        {
            runtimeStats.TryGetValue(type, out var stat);
            return stat;
        }

        // Applies a StatModifier to the matching Stat
        public void ApplyModifier(StatModifier mod)
        {
            if (mod == null) return;

            var stat = GetStat(mod.StatType);
            if (stat == null) return;

            float newValue = mod.ApplyTo(stat.CurrentValue);
            stat.Increase(newValue - stat.CurrentValue);
        }

        // Gets all stats for iteration
        public IReadOnlyDictionary<StatType, Stat> AllStats => runtimeStats;
    }
}
