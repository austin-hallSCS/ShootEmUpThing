using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardGame.Stats
{
    public class PlayerAbilities
    {
        public AbilityScore Strength { get; private set; }
        public AbilityScore Dexterity { get; private set; }
        public AbilityScore Constitution { get; private set; }
        public AbilityScore Intelligence { get; private set; }
        public AbilityScore Wisdom { get; private set; }
        public AbilityScore Charisma { get; private set; }

        private PlayerAbilityDataSO data;

        public static PlayerAbilities CopyFrom(PlayerAbilityDataSO playerAbilityData)
        {
            return new PlayerAbilities
            {
                data = playerAbilityData,

                Strength = new AbilityScore(playerAbilityData.StrengthStartValue, AbilityType.Strength),
                Dexterity = new AbilityScore(playerAbilityData.DexterityStartValue, AbilityType.Dexterity),
                Constitution = new AbilityScore(playerAbilityData.ConstitutionStartValue, AbilityType.Constitution),
                Intelligence = new AbilityScore(playerAbilityData.IntelligenceStartValue, AbilityType.Intelligence),
                Wisdom = new AbilityScore(playerAbilityData.WisdomStartValue, AbilityType.Wisdom),
                Charisma = new AbilityScore(playerAbilityData.CharismaStartValue, AbilityType.Charisma)
            };
        }
    }
}
