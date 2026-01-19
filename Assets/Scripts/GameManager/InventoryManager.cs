using System.Collections.Generic;
using UnityEngine;
using WizardGame.Spells;

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

        public SpellController GetSpellInstance(string spellName)
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

        public List<SpellController> GetEquippedSpells() => equippedSpells;
    }
}
