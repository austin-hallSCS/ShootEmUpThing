using System.Collections;
using UnityEngine;

namespace WizardGame.Spells
{
    public struct SpawnTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }
    public abstract class TargetingStrategySO : ScriptableObject
    {
        public abstract SpawnTransform GetSpawnTransform(SpellCastContext context, int index, int totalCount);
    }
}
