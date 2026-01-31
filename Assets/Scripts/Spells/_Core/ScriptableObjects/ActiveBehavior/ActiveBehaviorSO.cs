using UnityEngine;

namespace WizardGame.Spells
{
    public abstract class ActiveBehaviorSO : ScriptableObject
    {
        public abstract void Activate(SpellCastContext context);
        public abstract void SpellActiveBehavior();
        public abstract void Deactivate();
    }
}
