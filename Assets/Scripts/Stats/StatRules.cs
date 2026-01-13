using System.Collections.Generic;

namespace WizardGame.Stats
{
    /// <summary>
    /// Contains default configs for all Spell StatTypes. May add player Stats to this in the future.
    /// May also turn it into an SO, so that the default values can be edited in the inspector.
    /// </summary>
    public static class StatRules
    {
        public struct Config
        {
            public bool IsRounded;
            public bool IncreaseIsPositive;
            public bool IsCapChangeable;
            public float DefaultCap;
            public float DefaultMin;
        }

        // Default values
        private static readonly Dictionary<StatType, Config> rules = new()
        {
            {StatType.Rarity, new Config{ IsRounded = false, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 90f, DefaultMin =  1f} },
            {StatType.Damage, new Config{ IsRounded = false, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 100f, DefaultMin = 0f}},
            {StatType.Area, new Config{ IsRounded = false, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 3f, DefaultMin = 0.5f}},
            {StatType.Speed, new Config{ IsRounded = false, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 5f, DefaultMin = 0.5f}},
            {StatType.Cooldown, new Config{ IsRounded = false, IncreaseIsPositive = false, IsCapChangeable = false, DefaultCap = 60f, DefaultMin = 0.1f}},
            {StatType.Knockback, new Config{ IsRounded = false, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 2f, DefaultMin = 1f}},
            {StatType.Amount, new Config{ IsRounded = true, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 10f, DefaultMin = 1f}},
            {StatType.Duration, new Config{ IsRounded = false, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 10f, DefaultMin = 3f}},
            {StatType.Pierce, new Config{ IsRounded = true, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 10f, DefaultMin = 0f}},
        };

        public static Config Get(StatType type)
        {
            if (rules.TryGetValue(type, out var config)) return config;

            // Fallback default
            return new Config { IsRounded = false, IncreaseIsPositive = true, IsCapChangeable = false, DefaultCap = 100f, DefaultMin = 0f };
        }
    }
}
