using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using WizardGame.Utils;

namespace WizardGame.Managers
{
    public class UIManager : ManagerBase
    {
        private List<GameObject> currentUpgradeChoices;
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
            if (levelUpPanel == null) return;

            if (show)
            {
                // Generate upgrade choices
                currentUpgradeChoices = GetUpgradeOptions();

                // Pass new choices to the proxy
                if (levelUpPanel.TryGetComponent(out UI.LevelUpUIProxy proxy))
                {
                    proxy.UpdateUpgradeOptions(currentUpgradeChoices);
                }
            }

            // Activate the level up panel
            levelUpPanel.SetActive(show);
        }

        // Called by UIproxy script
        public void SelectUpgrade(int slotIndex)
        {
            if (currentUpgradeChoices == null || slotIndex >= currentUpgradeChoices.Count) return;

            GameObject selectedSpellPrefab = currentUpgradeChoices[slotIndex];
            Debug.Log($"Player selected: {selectedSpellPrefab.name}");

            EventManager.PublishLevelUpSelection(selectedSpellPrefab);
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
