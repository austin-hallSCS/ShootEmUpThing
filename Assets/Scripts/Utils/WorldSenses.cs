using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardGame.Utils
{
    public static class WorldSenses
    {
        public static Vector2 GetRandomDirection() => Vector2Int.RoundToInt(Random.insideUnitCircle.normalized);

        public static float GetSquareDistance(Vector3 target, Vector3 center) => (target - center).sqrMagnitude;
        public static float GetSquareDistance(Transform target, Transform center) => (target.position - center.position).sqrMagnitude;

        public static readonly List<Vector2> cardinalDirections = new List<Vector2>
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left, new Vector2(1f, 1f), new Vector2(1f, -1f), new Vector2(-1f, -1f), new Vector2(-1f, 1f)
        };
    }
}