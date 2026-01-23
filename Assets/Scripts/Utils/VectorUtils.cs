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
    }
}