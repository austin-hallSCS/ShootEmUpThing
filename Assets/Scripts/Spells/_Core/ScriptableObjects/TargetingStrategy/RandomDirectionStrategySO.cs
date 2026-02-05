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

            Vector3 source = context.Caster.position;
            float offset = context.Data.SpawnDistanceOffset;
            float currentAngle = randomRotation.eulerAngles.z;
            float rad = currentAngle * Mathf.Deg2Rad;

            // Calculate offset from player
            float x = Mathf.Cos(rad) * offset;
            float y = Mathf.Sin(rad) * offset;

            Debug.Log($"x: {x}, y: {y}");

            Vector3 spawnPoint = source + new Vector3(x, y, 0);

            return new SpawnTransform
            {
                Position = spawnPoint,
                Rotation = context.Caster.rotation * randomRotation
            };
        }
    }
}
