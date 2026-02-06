using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Utils
{
    public static class VectorUtils
    {
        public static Vector2 GetRandomDirection() => Random.insideUnitCircle.normalized;

        public static readonly List<Vector2> EightDirections = new List<Vector2>
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left,
            new Vector2(1f, 1f).normalized,
            new Vector2(1f, -1f).normalized,
            new Vector2(-1f, -1f).normalized,
            new Vector2(-1f, 1f).normalized
        };

        // Calculates a distance away from a source based on rotation
        public static Vector3 GetOffsetPosition(Vector3 source, float offset, Quaternion rotation)
        {
            float currentAngle = rotation.eulerAngles.z;
            float rad = currentAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(rad) * offset;
            float y = Mathf.Sin(rad) * offset;

            Debug.Log($"x: {x}, y: {y}");

            return source + new Vector3(x, y, 0);
        }

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