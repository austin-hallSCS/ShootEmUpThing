using UnityEngine;
using System;
using System.Collections.Generic;

namespace WizardGame.Stats
{
    public enum AbilityType{ Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma }

    [Serializable]
    public class AbilityScore
    {
        public int BaseValue { get; private set; }

        public AbilityType AbilityType { get; private set; }

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                // Min and Max values hardcoded
                int clamped = Mathf.Clamp(value, 1, 20);
                if (clamped != currentValue)
                {
                    currentValue = clamped;
                    CheckStatModifiers(); // Recalculate modifiers whenever score changes
                }
            }
        }

        private int currentValue;

        private List<StatModifier> modifiers = new List<StatModifier>();
        public IReadOnlyList<StatModifier> Modifiers => modifiers;

        public AbilityScore(int baseValue, AbilityType abilityType)
        {
            BaseValue = baseValue;
            AbilityType = abilityType;
            Init();
        }

        public void Init()
        {
            modifiers.Clear();

            switch (AbilityType)
            {
                case AbilityType.Strength:
                    modifiers.Add(new StatModifier(StatType.Damage));
                    modifiers.Add(new StatModifier(StatType.Knockback));
                    break;
                case AbilityType.Dexterity:
                    modifiers.Add(new StatModifier(StatType.MovementSpeed));
                    modifiers.Add(new StatModifier(StatType.Amount));
                    break;
                case AbilityType.Constitution:
                    modifiers.Add(new StatModifier(StatType.Health));
                    modifiers.Add(new StatModifier(StatType.DamageResistance));
                    break;
                case AbilityType.Intelligence:
                    modifiers.Add(new StatModifier(StatType.Area));
                    modifiers.Add(new StatModifier(StatType.Cooldown));
                    break;
                case AbilityType.Wisdom:
                    modifiers.Add(new StatModifier(StatType.Pierce));
                    modifiers.Add(new StatModifier(StatType.Speed));
                    break;
                case AbilityType.Charisma:
                    modifiers.Add(new StatModifier(StatType.Duration));
                    modifiers.Add(new StatModifier(StatType.Rarity));
                    break;
            }
            currentValue = BaseValue;
        }

        private void CheckStatModifiers()
        {
            // Determine modifier percentage based on Ability Score
            float percentChange = GetPercentChangeForScore(currentValue);

            foreach (var mod in modifiers)
            {
                // Reset before applying new logic
                mod.Reset();

                mod.SetValueType(ValueType.Percent);
                mod.SetValue(percentChange);

                if (currentValue < 10)
                {
                    mod.SetModType(ModifierType.Penalty);
                }
                else if (currentValue > 10)
                {
                    mod.SetModType(ModifierType.Bonus);
                }
                else
                {
                    mod.SetValue(0f);
                }
            }
        }

        private float GetPercentChangeForScore(int score)
        {
            return score switch
            {
                1 or 20 => 50f,
                2 or 3 or 18 or 19 => 40f,
                4 or 5 or 16 or 17 => 30f,
                6 or 7 or 14 or 15 => 20f,
                8 or 9 or 12 or 13 => 10f,
                10 or 11 => 0f,
                _ => 0f

            };
        }
    }
}
