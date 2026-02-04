using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Utils
{
    public static class VectorUtils
    {
        public static Vector2 GetRandomDirection() => Random.insideUnitCircle.normalized;

        public static readonly List<Vector2> eightDirections = new List<Vector2>
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left,
            new Vector2(1f, 1f).normalized,
            new Vector2(1f, -1f).normalized,
            new Vector2(-1f, -1f).normalized,
            new Vector2(-1f, 1f).normalized
        };

        public static Vector3 BezierCalculation(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            //-- BEZIER FORMULA --
            // Position = (1-t)^2 * Start + 2(1-t)t * Control + t^2 * End

            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
            return p;
        }
    }
}