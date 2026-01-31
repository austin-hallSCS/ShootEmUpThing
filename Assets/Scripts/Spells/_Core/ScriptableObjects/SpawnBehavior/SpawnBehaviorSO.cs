using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    public abstract class SpawnBehaviorSO : ScriptableObject
    {
        public abstract IEnumerator Execute(SpellCastContext ctx, IActiveBehavior activeBehavior);
    }
}
