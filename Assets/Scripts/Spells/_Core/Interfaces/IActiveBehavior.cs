using UnityEngine;

namespace WizardGame.Spells
{
    public interface IActiveBehavior
    {
        void Activate(SpellCastContext context);
        void SpellActiveBehavior();
        void Deactivate();
    }
}
