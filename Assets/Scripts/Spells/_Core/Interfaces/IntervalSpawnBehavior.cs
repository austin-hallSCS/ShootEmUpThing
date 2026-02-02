using System.Collections;
using UnityEngine;
using WizardGame.Services;

namespace WizardGame.Spells
{
    public class IntervalSpawnBehavior : ISpawnBehavior
    {
        // Spawn game objects equal to the Amount stat, register them with the context
        public IEnumerator Execute(SpellCastContext context)
        {
            int count = (int)context.Stats.GetStat(Stats.StatType.Amount).CurrentValue;
            float delay = context.Data.ProjectileIntervalTime;
            GameObject prefab = context.Data.SpellPrefab;
            Vector3 position = context.Caster.position;
            Quaternion rotation = context.Caster.rotation;

            for (int i = 0; i < count; i++)
            {
                //TODO: Figure out targeting logic to pass in target and rotation
                GameObject instance = PoolService.Spawn(prefab, position, rotation);

                if (instance.TryGetComponent(out SpellGO spellGO))
                {
                    spellGO.Initialize(context.Data, context.Stats);
                }

                // Track the spawned instance to be despawned later
                context.SpawnedInstances.Add(instance);

                yield return new WaitForSeconds(delay);
            }
        }

        // Despawn all instances in the context
        public void Despawn(SpellCastContext context)
        {
            foreach (var instance in context.SpawnedInstances)
            {
                if (instance != null && instance.activeInHierarchy)
                {
                    PoolService.Despawn(instance, context.Data.SpellPrefab);
                }
            }
        }
    }
}
