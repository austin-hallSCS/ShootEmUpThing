using UnityEngine;

namespace WizardGame.Spells
{
    public abstract class TargetingStrategySO : ScriptableObject
    {
        public abstract Pose GetPose(SpellCastContext context, int index, int totalCount);
    }
}
