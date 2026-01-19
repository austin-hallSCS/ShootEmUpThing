using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.Managers;
using WizardGame.Spells;
using WizardGame.Utils;

namespace WizardGame
{
    public class SpellManager : ManagerBase
    {

        private List<GameObject> spellPool;

        public SpellManager(GameManager manager) : base(manager)
        {
            // Copy the spell database
            spellPool = gameManager.AllSpellsDatabase.AllSpellPrefabs.ToList();

            SubscribeToEvents();
        }
        protected override void SubscribeToEvents()
        {

        }
        protected override void UnsubscribeFromEvents() { }

        public List<GameObject> GetUpgradeOptions()
        {
            // TODO: Add weight to spells based on rarity

            // Make new shuffle bag with all spells
            ShuffleBag<GameObject> upgradeBag = new ShuffleBag<GameObject>(spellPool);
            List<GameObject> choices = new List<GameObject>();

            // Get 3 spells from the shuffle bag
            for (var i = 0; i < 3; i++)
            {
                choices.Add(upgradeBag.GetNext());
            }

            return choices;
        }

        // Generate a string for the Level up option UI elements
        public string GetLevelUpDescriptions(string spellName)
        {
            // Get reference to the spell instance
            SpellController spellInstance = gameManager.GetManager<InventoryManager>().GetSpellInstance(spellName);

            if (spellInstance == null) return spellName;

            // Get level data for the next level
            int nextLevel = spellInstance.SpellStats.Level + 1;
            SpellLevelData levelData = spellInstance.SpellData.GetLevelData(nextLevel);

            var sb = new System.Text.StringBuilder();

            sb.Append($"Level {nextLevel}: ");

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

        private void RemoveSpellFromPool(GameObject spell)
        {
            if (!spellPool.Contains(spell)) return;

            spellPool.Remove(spell);
        }
    }
}
