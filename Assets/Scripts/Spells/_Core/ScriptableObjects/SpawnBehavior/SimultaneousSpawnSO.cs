using System.Collections;
using UnityEngine;
using WizardGame.Stats;
using WizardGame.Services;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "SimultaneousSpawn_Asset", menuName = "Spells/Spawn Behaviors/Simultaneous")]
    public class SimultaneousSpawnSO : SpawnBehaviorSO
    {
        public override IEnumerator Execute(SpellCastContext context)
        {
            int count = (int)context.Stats.GetStat(Stats.StatType.Amount).CurrentValue;
            GameObject prefab = context.Data.SpellPrefab;
            Vector3 position = context.Caster.position;
            Quaternion rotation = context.Caster.rotation;

            // Spawn them all in one frame
            for (int i = 0; i < count; i++)
            {
                // TODO: Figure out targeting logic to pass in target and rotation
                GameObject gameObject = PoolService.Spawn(prefab, position, rotation);
                gameObject.GetComponent<SpellGO>().Initialize(context.Data, context.Stats);
            }
            yield break; // Done immediately
        }
    }
}
