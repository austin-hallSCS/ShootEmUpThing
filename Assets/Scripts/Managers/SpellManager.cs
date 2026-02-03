using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.Managers;
using WizardGame.Spells;
using WizardGame.Utils;
using WizardGame.Services;

namespace WizardGame
{
    public class SpellManager : ManagerBase
    {

        private List<SpellDataSO> spellPool;

        public SpellManager(GameManager manager) : base(manager)
        {
            // Copy the database
            spellPool = gameManager.AllSpellsDatabase.AllSpellsData.ToList();
            SubscribeToEvents();
        }

        protected override void SubscribeToEvents()
        {
            EventBus.OnSpellMaxLevel += RemoveSpellFromPool;
        }
        protected override void UnsubscribeFromEvents()
        {
            EventBus.OnSpellMaxLevel -= RemoveSpellFromPool;
        }

        public List<SpellDataSO> GetUpgradeOptions()
        {
            // TODO: Add weight to spells based on rarity

            // Make new shuffle bag with all spells
            ShuffleBag<SpellDataSO> upgradeBag = new ShuffleBag<SpellDataSO>(spellPool);
            List<SpellDataSO> choices = new List<SpellDataSO>();

            // Get 3 spells from the shuffle bag
            for (var i = 0; i < 3; i++)
            {
                if (i > spellPool.Count)
                {
                    break;
                }
                choices.Add(upgradeBag.GetNext());
            }

            foreach (var choice in choices)
            {
                Debug.Log($"Choice: {choice.SpellName}");
            }
            return choices;
        }

        // Generate a string for the Level up option UI elements
        public string GetLevelUpDescriptions(SpellDataSO spellData)
        {
            // Get reference to spell instance
            SpellController spellInstance = gameManager.GetManager<InventoryManager>().GetSpellInstance(spellData);

            // If spell is not equipped, return the Spell's description
            if (spellInstance == null) return spellData.Description;

            // Get level data for the next level
            int nextLevel = spellInstance.SpellStats.Level + 1;
            Debug.Log($"{spellData.SpellName} nextLevel: {nextLevel}");
            SpellLevelData levelData = spellData.GetLevelData(nextLevel);

            var sb = new System.Text.StringBuilder();

            sb.Append($"Level {nextLevel}: ");

            if (spellData == null)
            {
                Debug.LogWarning("Spell Data is null!");
            }
            for (int i = 0; i < levelData.Modifiers.Count; i++)
            {
                sb.Append(levelData.Modifiers[i].GenerateDescription());

                if (i != (levelData.Modifiers.Count - 1))
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        private void RemoveSpellFromPool(SpellDataSO dataToFind)
        {
            foreach (var spell in spellPool)
            {
                if (spell == dataToFind)
                {
                    spellPool.Remove(spell);
                    Debug.Log($"Spell Removed: {spell.SpellName}");
                    break;
                }
            }
        }
    }
}
