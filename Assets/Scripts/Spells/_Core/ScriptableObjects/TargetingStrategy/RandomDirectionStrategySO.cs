using UnityEngine;

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

            Quaternion randomRotation = Quaternion.Euler(0, randomAngle, 0);

            return new SpawnTransform
            {
                Position = context.Caster.position,
                Rotation = context.Caster.rotation * randomRotation
            };
        }
    }
}
