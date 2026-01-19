using System.Collections.Generic;
using UnityEngine;
using WizardGame.Spells;
using WizardGame.Utils;

namespace WizardGame.Managers
{
    public class InventoryManager : ManagerBase
    {
        private List<SpellController> equippedSpells;

        public List<GameObject> SpellsAvailableForLevelUp;

        public InventoryManager(GameManager manager) : base(manager)
        {
            equippedSpells = new List<SpellController>();

            SubscribeToEvents();
        }

        protected override void SubscribeToEvents()
        {
            EventManager.OnLevelUpSelection += ProcessLevelUp;
        }

        protected override void UnsubscribeFromEvents()
        {
            EventManager.OnLevelUpSelection -= ProcessLevelUp;
        }

        public void ProcessLevelUp(GameObject spellPrefab)
        {
            if (spellPrefab == null) return;

            SpellController prefabController = spellPrefab.GetComponent<SpellController>();
            if (prefabController == null)
            {
                Debug.LogError($"Prefab {spellPrefab.name} is missing a SpellController!");
            }

            SpellController exsistingSpell = GetSpellInstance(prefabController.SpellData.SpellName);
            if (exsistingSpell != null)
            {
                Debug.Log($"Leveling up existing spell: {exsistingSpell.SpellData.SpellName}");
                exsistingSpell.LevelUp();
            }
            else
            {
                Debug.Log($"Equipping new spell: {prefabController.SpellData.SpellName}");
                InstantiateNewSpell(spellPrefab);
            }
        }

        private void InstantiateNewSpell(GameObject prefab)
        {
            if (gameManager.PlayerController == null)
            {
                Debug.LogWarning("PlayerController is null on InventoryManager's GameManager");
                return;
            }

            Transform playerTransform = gameManager.PlayerController.transform;
            GameObject spellControllerObject = Object.Instantiate(prefab, playerTransform);
            SpellController controller = spellControllerObject.GetComponent<SpellController>();

            controller.Initialize(gameManager.PlayerController.PlayerAbilities);

            equippedSpells.Add(controller);

            Debug.Log($"Added new spell: {spellControllerObject.name}");
        }

        private SpellController GetSpellInstance(string spellName)
        {
            foreach (var spell in equippedSpells)
            {
                if (spell.SpellData.SpellName == spellName)
                {
                    return spell;
                }
            }
            return null;
        }

        // Generate a string for the Level up option UI elements
        public string GetLevelUpDescriptions(string spellName)
        {
            // Get reference to the spell instance
            SpellController spellInstance = GetSpellInstance(spellName);

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

        public List<GameObject> GetUpgradeOptions()
        {
            // TODO: Add weight to spells based on rarity

            // Make new shuffle bag with all spells
            ShuffleBag<GameObject> upgradeBag = new ShuffleBag<GameObject>(gameManager.AllSpellsDatabase.AllSpellPrefabs);
            List<GameObject> choices = new List<GameObject>();

            // Get 3 spells from the shuffle bag
            for (var i = 0; i < 3; i++)
            {
                choices.Add(upgradeBag.GetNext());
            }

            return choices;
        }

        public List<SpellController> GetEquippedSpells() => equippedSpells;
    }
}
