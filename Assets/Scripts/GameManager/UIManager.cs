using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utils;

namespace WizardGame.Managers
{
    public class UIManager : ManagerBase
    {
        private GameObject levelUpPanel;
        public UIManager(GameManager manager, GameObject panelUI) : base(manager)
        {
            levelUpPanel = panelUI;
            SubscribeToEvents();
        }
        protected override void SubscribeToEvents()
        {
            EventManager.OnPlayerLevelUp += _ => ShowLevelUpElement(true);
            EventManager.OnGameResumed += () => ShowLevelUpElement(false);
        }

        protected override void UnsubscribeFromEvents()
        {
            EventManager.OnPlayerLevelUp -= _ => ShowLevelUpElement(true);
            EventManager.OnGameResumed -= () => ShowLevelUpElement(false);
        }

        protected void ShowLevelUpElement(bool show)
        {
            if (levelUpPanel != null)
                levelUpPanel.SetActive(show);
        }

        // Called by UIproxy script
        public void SelectUpgrade(int slotIndex)
        {
            Debug.Log($"Player selected upgrade for slot {slotIndex}");

            // TODO: Tell InventoryManager or PlayerController to apply upgrade

            EventManager.PublishGameResumed();
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
    }
}
