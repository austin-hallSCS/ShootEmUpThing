using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Utils
{
    public static class WorldSenses
    {
        public static Vector2 GetRandomDirection() => Vector2Int.RoundToInt(Random.insideUnitCircle.normalized);

        public static readonly List<Vector2> cardinalDirections = new List<Vector2>
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left, new Vector2(1f, 1f), new Vector2(1f, -1f), new Vector2(-1f, -1f), new Vector2(-1f, 1f)
        };

        public static Quaternion VectorDirectionToRotation(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public static float VectorDirectionToCardinalRotation(Vector2 direction)
        {
            switch ((Vector2)direction)
            {
                case Vector2 v when v.Equals(Vector2.up): // North
                    return 0f;
                case Vector2 v when v.Equals(new Vector2(1f, 1f)): // Northeast
                    return 315;
                case Vector2 v when v.Equals(Vector2.right): // East
                    return 270f;
                case Vector2 v when v.Equals(new Vector2(1f, -1f)): // Southeast
                    return 225f;
                case Vector2 v when v.Equals(Vector2.down): // South
                    return 180f;
                case Vector2 v when v.Equals(new Vector2(-1f, -1f)): // Southwest
                    return 135f;
                case Vector2 v when v.Equals(Vector2.left): // West
                    return 90f;
                case Vector2 v when v.Equals(new Vector2(-1f, 1f)): // Northwest
                    return 45f;
                default:
                    return 0f;
            }
        }
    }
}