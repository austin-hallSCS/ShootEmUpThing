using UnityEngine;

namespace WizardGame.Stats
{
    public class PlayerStats
    {
        public int Level { get; private set; }

        public Stat MovementSpeed { get; private set; }
        public Stat Health { get; private set; }
        public Stat ExperienceAmount { get; private set; }


        [Header("Ability Scores")]
        public AbilityScore Strength { get; private set; }
        public AbilityScore Dexterity { get; private set; }
        public AbilityScore Constitution { get; private set; }
        public AbilityScore Intelligence { get; private set; }
        public AbilityScore Wisdom { get; private set; }
        public AbilityScore Charisma { get; private set; }

        private PlayerDataSO data;

        public static PlayerStats CopyFrom(PlayerDataSO playerData)
        {
            return new PlayerStats
            {
                data = playerData,

                Level = 1,

                Health = new Stat(StatType.Health, true, false, 100, 0, 100),

                Strength = new AbilityScore()



            };
        }
    }
}
