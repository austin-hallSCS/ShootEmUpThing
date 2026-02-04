using UnityEngine;
using UnityEngine.Rendering;

namespace WizardGame.Spells
{
    /// <summary>
    /// Returns a random direction within a slice of a circle determined by index and total count.
    /// Ensures objects do not spawn overlapping while retaining randomness
    /// </summary>
    [CreateAssetMenu(fileName = "RandomDirection_Asset", menuName = "Spells/Targeting/Random Direction")]
    public class RandomDirectionStrategy : TargetingStrategySO
    {
        public override SpawnTransform GetSpawnTransform(SpellCastContext context, int index, int totalCount)
        {
            float sliceSize = 360f / totalCount;

            float minAngle = sliceSize * index;
            float maxAngle = sliceSize * (index + 1);

            float randomAngle = Random.Range(minAngle, maxAngle);

            Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);

            // TODO: Fix this, doesn't work
            // Apply spawn offset
            Vector3 source = context.Caster.position;
            float offset = context.Data.SpawnDistanceOffset;
            Vector3 direction = randomRotation.eulerAngles;
            Debug.Log($"direction: {direction}");

            Vector3 spawnPoint = source + (direction.normalized * offset);

            Debug.Log($"offset: {direction * offset}");
            Debug.Log($"spawnPoint: {spawnPoint}");

            return new SpawnTransform
            {
                Position = spawnPoint,
                Rotation = context.Caster.rotation * randomRotation
            };
        }
    }
}
