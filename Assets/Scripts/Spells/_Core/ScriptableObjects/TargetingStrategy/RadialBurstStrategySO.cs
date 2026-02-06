using UnityEngine;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "RadialBurst_Asset", menuName = "Spells/Targeting/Radial Burst")]
    public class RadialBurstStrategySO : TargetingStrategySO
    {
        public override Pose GetPose(SpellCastContext context, int index, int totalCount)
        {
            float angleStep = 360f / totalCount;
            float angle = index * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            return new Pose
            {
                position = context.Caster.position,
                rotation = rotation
            };
        }
    }
}
