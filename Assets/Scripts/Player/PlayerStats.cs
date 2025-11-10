using UnityEngine;

namespace WizardGame.Stats
{
    public class PlayerStats
    {
        public int Level { get; private set; }
        public float MovementSpeed { get; private set; }

        public Stat Health { get; private set; }
        public Stat ExperienceAmount { get; private set; }


        [Header("Ability Scores")]
        public Stat Strength { get; private set; }
        public Stat Dexterity { get; private set; }
        public Stat Constitution { get; private set; }
        public Stat Intelligence { get; private set; }
        public Stat Wisdom { get; private set; }
        public Stat Charisma { get; private set; }
    }
}
