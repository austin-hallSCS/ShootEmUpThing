using UnityEngine;

namespace WizardGame.Utils
{
    public static class TransformExtensions
    {
        public static float GetSquareDistance(this Transform source, Transform target)
        {
            return (target.position - source.position).sqrMagnitude;
        }
    }
}
