using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    public interface ISpawnBehavior
    {
        IEnumerator Execute(SpellCastContext context);

        void Despawn(SpellCastContext context);
    }
}
