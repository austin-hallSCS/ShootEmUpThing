using System.Collections;
using UnityEngine;
using WizardGame.Services;

namespace WizardGame.Spells
{
    public class SpellEmitter : ISpellEmitter
    {
        // Spawn game objects equal to the Amount stat, register them with the context
        public IEnumerator Execute(SpellCastContext context)
        {
            int count = (int)context.Stats.GetStat(Stats.StatType.Amount).CurrentValue;
            float delay = context.Data.ProjectileIntervalTime;
            GameObject prefab = context.Data.SpellPrefab;

            TargetingStrategySO targeting = context.Data.TargetingStrategy;

            for (int i = 0; i < count; i++)
            {
                SpawnTransform spawnInfo = targeting.GetSpawnTransform(context, i, count);

                //TODO: Figure out targeting logic to pass in target and rotation
                GameObject instance = PoolService.Spawn(prefab, spawnInfo.Position, spawnInfo.Rotation);

                if (instance.TryGetComponent(out SpellGO spellGO))
                {
                    spellGO.Initialize(context);
                }

                // Track the spawned instance to be despawned later
                context.SpawnedInstances.Add(instance);

                // Only delay if delay is greater than 0
                if (delay > Mathf.Epsilon)
                {
                    yield return new WaitForSeconds(delay);
                }
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
