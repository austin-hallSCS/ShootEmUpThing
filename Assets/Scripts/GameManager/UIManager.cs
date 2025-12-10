using UnityEngine;

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
    }
}
