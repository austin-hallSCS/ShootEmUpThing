using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "IntervalSpawnSO", menuName = "Spells/Spawn Behaviors/Simultaneous")]
    public class SimultaneousSpawnSO : SpawnBehaviorSO
    {
        public override IEnumerator Execute(SpellCastContext context, IActiveBehavior activeBehavior)
        {

        }
    }
}
