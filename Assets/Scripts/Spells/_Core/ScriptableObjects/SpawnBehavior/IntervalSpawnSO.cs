using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "IntervalSpawnSO", menuName = "Spells/Spawn Behaviors/Interval")]
    public class IntervalSpawnSO : SpawnBehaviorSO
    {
        public override IEnumerator Execute(SpellCastContext context, IActiveBehavior activeBehavior)
        {
            int count = (int)context.Stats.GetStat(Stats.StatType.Amount).CurrentValue;
            float delay = context.Data.ProjectileIntervalTime;

            for (int i = 0; i < count; i++)
            {
                activeBehavior.Activate(context);

                yield return new WaitForSeconds(delay);
            }
        }
    }
}
