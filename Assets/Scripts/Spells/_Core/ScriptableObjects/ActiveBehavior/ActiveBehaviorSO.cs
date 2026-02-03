using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    public abstract class ActiveBehaviorSO : ScriptableObject
    {
        public abstract IEnumerator Activate(SpellCastContext context, ISpellEmitter spawnBehavior);
        public abstract void Deactivate(SpellCastContext context, ISpellEmitter spawnBehavior);
    }
}
