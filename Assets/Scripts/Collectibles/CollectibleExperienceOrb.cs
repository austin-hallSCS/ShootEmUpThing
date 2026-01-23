using UnityEngine;
using WizardGame.Services;

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

            EventBus.PublishExperienceCollected(xpAmount);
        }
    }
}
