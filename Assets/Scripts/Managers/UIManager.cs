using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utils;
using WizardGame.Services;

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
            EventBus.OnPlayerLevelUp += _ => ShowLevelUpElement(true);
            EventBus.OnGameResumed += () => ShowLevelUpElement(false);
        }

        protected override void UnsubscribeFromEvents()
        {
            EventBus.OnPlayerLevelUp -= _ => ShowLevelUpElement(true);
            EventBus.OnGameResumed -= () => ShowLevelUpElement(false);
        }

        protected void ShowLevelUpElement(bool show)
        {
            if (levelUpPanel == null) return;

            if (show)
            {
                // Generate upgrade choices
                currentUpgradeChoices = gameManager.GetManager<SpellManager>().GetUpgradeOptions();

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

            EventBus.PublishLevelUpSelection(selectedSpellPrefab);
            EventBus.PublishGameResumed();
        }
    }
}
