using System.Collections.Generic;
using UnityEngine;
using WizardGame.Player;
using WizardGame.Spells;

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

        }

        protected override void UnsubscribeFromEvents()
        {

        }

        public void AddSpell(GameObject spellControllerPrefab)
        {
            if (gameManager.PlayerController == null)
            {
                Debug.LogWarning("PlayerController is null on InventoryManager's GameManager");
                return;
            }

            Transform playerTransform = gameManager.PlayerController.transform;
            GameObject spellControllerObject = Object.Instantiate(spellControllerPrefab, playerTransform);
            SpellController controller = spellControllerObject.GetComponent<SpellController>();

            controller.Initialize(gameManager.PlayerController.PlayerAbilities);

            equippedSpells.Add(controller);

            Debug.Log($"Added new spell: {spellControllerObject.name}");
        }

        public bool HasSpell(SpellController spell)
        {
            return equippedSpells.Contains(spell);
        }

        public List<SpellController> GetEquippedSpells()
        {
            return equippedSpells;
        }
    }
}
