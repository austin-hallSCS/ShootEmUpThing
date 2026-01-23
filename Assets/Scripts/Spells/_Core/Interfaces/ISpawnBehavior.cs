using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    public interface ISpawnBehavior
    {
        IEnumerator Execute(SpellCastContext ctx, IActiveBehavior activeBehavior);
        public void Spawn();
    }
}
