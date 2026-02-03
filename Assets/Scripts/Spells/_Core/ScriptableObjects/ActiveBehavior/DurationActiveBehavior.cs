using System.Collections;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    /// <summary>
    /// Class used to define behavior for spells that use duration time.
    /// Asks the SpawnBehavior to spawn gameobjects, then despawns them when the duration ends.
    /// </summary>
    [CreateAssetMenu(fileName = "DurationActiveBehavior_Asset", menuName = "Spells/Active Behaviors/Duration")]
    public class DurationActiveBehaviorSO : ActiveBehaviorSO
    {
        public override IEnumerator Activate(SpellCastContext context, ISpellEmitter spawnBehavior)
        {
            float duration = context.Stats.GetStat(StatType.Duration).CurrentValue;

            // Have the controller start the spawn coroutine
            context.Controller.StartCoroutine(spawnBehavior.Execute(context));

            // Begin duration Countdown
            yield return new WaitForSeconds(duration);

            // Deactivate spell once duration ends
            Deactivate(context, spawnBehavior);
        }

        public override void Deactivate(SpellCastContext context, ISpellEmitter spawnBehavior)
        {
            // Despawn all game objects within the context
            spawnBehavior.Despawn(context);
        }
    }
}
