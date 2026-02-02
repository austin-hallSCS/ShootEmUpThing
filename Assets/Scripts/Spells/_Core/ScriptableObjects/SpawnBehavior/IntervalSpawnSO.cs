using System.Collections;
using UnityEngine;
using WizardGame.Services;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "IntervalSpawn_Asset", menuName = "Spells/Spawn Behaviors/Interval")]
    public class IntervalSpawnSO : SpawnBehaviorSO
    {
        public override IEnumerator Execute(SpellCastContext context)
        {
            int count = (int)context.Stats.GetStat(Stats.StatType.Amount).CurrentValue;
            float delay = context.Data.ProjectileIntervalTime;
            GameObject prefab = context.Data.SpellPrefab;
            Vector3 position = context.Caster.position;
            Quaternion rotation = context.Caster.rotation;

            for (int i = 0; i < count; i++)
            {
                //TODO: Figure out targeting logic to pass in target and rotation
                GameObject gameObject = PoolService.Spawn(prefab, position, rotation);
                gameObject.GetComponent<SpellGO>().Initialize(context.Data, context.Stats);

                yield return new WaitForSeconds(delay);
            }
        }
    }
}
