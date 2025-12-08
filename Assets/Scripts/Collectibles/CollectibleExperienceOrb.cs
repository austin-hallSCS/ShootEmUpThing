using UnityEngine;
using WizardGame.Managers;

namespace WizardGame.Collectibles
{
    public class CollectibleExperienceOrb : Collectible
    {
        // Data
        private int xpAmount;

        public void SetXPAmount(int amount)
        {
            xpAmount = amount;
        }

        public override void OnCollected()
        {
            base.OnCollected();

            EventManager.PublishExperienceCollected(xpAmount);
        }
    }
}
