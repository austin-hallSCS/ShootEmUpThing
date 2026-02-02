using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    public abstract class ActiveBehaviorSO : ScriptableObject
    {
        public abstract IEnumerator Activate(SpellCastContext context, ISpawnBehavior spawnBehavior);
        public abstract void Deactivate(SpellCastContext context, ISpawnBehavior spawnBehavior);
    }
}
