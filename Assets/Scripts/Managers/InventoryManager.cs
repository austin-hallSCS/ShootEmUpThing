using System.Collections.Generic;
using UnityEngine;
using WizardGame.Spells;
using WizardGame.Services;
using WizardGame.Player;

namespace WizardGame.Managers
{
    public class InventoryManager : ManagerBase
    {
        private List<SpellController> equippedSpells;

        public InventoryManager(GameManager manager) : base(manager)
        {
            equippedSpells = new List<SpellController>();

            SubscribeToEvents();
        }

        protected override void SubscribeToEvents()
        {
            EventBus.OnLevelUpSelection += ProcessLevelUp;
        }

        protected override void UnsubscribeFromEvents()
        {
            EventBus.OnLevelUpSelection -= ProcessLevelUp;
        }

        public void ProcessLevelUp(SpellDataSO spellData)
        {
            if (spellData == null) return;

            SpellController exsistingSpell = GetSpellInstance(spellData);
            if (exsistingSpell != null)
            {
                Debug.Log($"Leveling up existing spell: {exsistingSpell.SpellData.SpellName}");
                exsistingSpell.LevelUp();

                Debug.Log($"Spell level after level up: {exsistingSpell.SpellStats.Level}");

                if (exsistingSpell.SpellStats.Level >= 10)
                {
                    EventBus.PublishSpellMaxLevel(exsistingSpell.SpellData);
                }
            }
            else
            {
                Debug.Log($"Equipping new spell: {spellData.SpellName}");
                InstantiateNewSpell(spellData);
            }
        }

        private void InstantiateNewSpell(SpellDataSO spellData)
        {
            if (gameManager.PlayerController == null)
            {
                Debug.LogWarning("PlayerController is null on InventoryManager's GameManager");
                return;
            }

            SpellController newSpellController = BuildNewController(spellData);

            equippedSpells.Add(newSpellController);

            Debug.Log($"Added new spell: {newSpellController.name}");
        }
        
        private SpellController BuildNewController(SpellDataSO spellData)
        {
            if (gameManager.PlayerController == null)
            {
                Debug.LogWarning("PlayerController is null on InventoryManager's GameManager");
                return null;
            }
            PlayerController player = gameManager.PlayerController;
            string objectName = $"{spellData.SpellName}Controller";

            // Create new game object
            GameObject newObject = new GameObject(objectName, typeof(SpellController));

            // Initialize controller
            SpellController newController = newObject.GetComponent<SpellController>();
            newController.Initialize(spellData, player);

            return newController;
        }

        public SpellController GetSpellInstance(SpellDataSO dataToFind)
        {
            foreach (var spell in equippedSpells)
            {
                if (spell.SpellData == dataToFind)
                {
                    return spell;
                }
            }
            return null;
        }

        public List<SpellController> GetEquippedSpells() => equippedSpells;
    }
}
