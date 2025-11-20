using System;
using UnityEngine;

namespace WizardGame.Managers
{
    public class XPManager : ManagerBase
    {
        public event Action OnPlayerLevelUp;

        public int CurrentPlayerLevel{ get; private set; }
        public int CurrentXP { get; private set; }
        public int XPToNextLevel { get; private set; }

        public XPManager()
        {
            CurrentPlayerLevel = 1;
            CurrentXP = 0;
            XPToNextLevel = 5;

            SubscribeToEvents();
        }

        public void AddExperience(int amount)
        {
            CurrentXP += amount;
            Debug.Log($"Experience added: {CurrentXP}");
            CheckForLevelUp();
        }

        private void CheckForLevelUp()
        {
            if (CurrentXP >= XPToNextLevel)
            {
                OnPlayerLevelUp?.Invoke();

                int overflow = CurrentXP - XPToNextLevel;

                CurrentPlayerLevel++;

                // FIXME: make this not a magic number
                XPToNextLevel += 10;

                CurrentXP = 0;

                AddExperience(overflow);
            }
        }

        protected override void SubscribeToEvents()
        {
            EventManager.OnExperienceCollected += AddExperience;
        }

        protected override void UnsubscribeFromEvents()
        {
            EventManager.OnExperienceCollected -= AddExperience;
        }

        public override void TearDown()
        {
            UnsubscribeFromEvents();
        }
    }
}
