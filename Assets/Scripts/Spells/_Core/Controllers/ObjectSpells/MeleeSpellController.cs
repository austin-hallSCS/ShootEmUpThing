using UnityEngine;

namespace WizardGame.Spells
{
    public class MeleeSpellController : ObjectSpellController
    {
        protected override void SpellActiveBehavior()
        {
            base.SpellActiveBehavior();

            currentDurationTimeAt -= Time.deltaTime;

            if (currentDurationTimeAt <= 0)
            {
                SpellDeactivate();
            }
        }
    }
}
