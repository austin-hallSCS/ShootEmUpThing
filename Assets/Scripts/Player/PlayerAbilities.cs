using System;
using System.Collections.Generic;

namespace WizardGame.Stats
{
    public class PlayerAbilities
    {
        public event Action OnAbilityModifiersUpdated;

        public AbilityScore Strength { get; private set; }
        public AbilityScore Dexterity { get; private set; }
        public AbilityScore Constitution { get; private set; }
        public AbilityScore Intelligence { get; private set; }
        public AbilityScore Wisdom { get; private set; }
        public AbilityScore Charisma { get; private set; }

        protected readonly List<AbilityScore> allScores = new();
        protected readonly List<StatModifier> allModifiers = new();

        public IReadOnlyList<AbilityScore> AllScores => allScores;
        public IReadOnlyList<StatModifier> AllModifiers => allModifiers;

        public PlayerAbilities(PlayerAbilityDataSO baseData)
        {
            Strength = new AbilityScore((int)baseData.StrengthStartValue, AbilityType.Strength);
            allScores.Add(Strength);

            Dexterity = new AbilityScore((int)baseData.DexterityStartValue, AbilityType.Dexterity);
            allScores.Add(Dexterity);

            Constitution = new AbilityScore((int)baseData.ConstitutionStartValue, AbilityType.Constitution);
            allScores.Add(Constitution);

            Intelligence = new AbilityScore((int)baseData.IntelligenceStartValue, AbilityType.Intelligence);
            allScores.Add(Intelligence);

            Wisdom = new AbilityScore((int)baseData.WisdomStartValue, AbilityType.Wisdom);
            allScores.Add(Wisdom);

            Charisma = new AbilityScore((int)baseData.CharismaStartValue, AbilityType.Charisma);
            allScores.Add(Charisma);

            // Subscribe to OnScoreChanged event for all scores, and add their modifiers to the list
            foreach (var score in AllScores)
            {
                score.OnScoreChanged += HandleScoreChanged;
                allModifiers.AddRange(score.Modifiers);
            }
        }

        private void HandleScoreChanged()
        {
            // Reset allModifiers list and repopulate with new modifiers
            allModifiers.Clear();
            foreach (var score in AllScores)
            {
                allModifiers.AddRange(score.Modifiers);
            }

            OnAbilityModifiersUpdated?.Invoke();
        }
    }
}
