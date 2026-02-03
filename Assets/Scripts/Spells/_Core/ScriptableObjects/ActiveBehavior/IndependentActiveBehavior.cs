using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "IndependentActiveBehavior_Asset", menuName = "Spells/Active Behaviors/Independent")]
    public class IndependentActiveBehavior : ActiveBehaviorSO
    {
        public override IEnumerator Activate(SpellCastContext context, ISpellEmitter spawnBehavior)
        {
            // Have the controller start the spawn coroutine
            context.Controller.StartCoroutine(spawnBehavior.Execute(context));

            // Deactivate once spell is over
            Deactivate(context, spawnBehavior);

            yield break;
        }

        public override void Deactivate(SpellCastContext context, ISpellEmitter spawnBehavior) { }
    }
}
