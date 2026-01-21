using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utils;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    /// <summary>
    /// Controls any spells that instantiate a GameObject that interacts with physics triggers
    /// </summary>
    public class ObjectSpellController : SpellController
    {
        protected override void SpellActiveBehavior() { }

        protected override void SpellActivate()
        {
            base.SpellActivate();

            List<TransformData> spawnPoints = GetSpawnData(SpellData.Targeting);

            foreach (var point in spawnPoints)
            {
                GameObject gameObject = Instantiate(spellPrefab, point.Position, point.Rotation);

                var script = gameObject.GetComponent<SpellGO>();
                script.Initialize(this);
            }
        }

        private List<TransformData> GetSpawnData(TargetingStyle style)
        {
            var results = new List<TransformData>();
            Vector3 playerPos = transform.position;

            switch (style)
            {
                case TargetingStyle.NearestEnemy:
                    Transform target = GetNearestEnemy();
                    if (target != null)
                    {
                        Vector2 direction = (target.position - playerPos).normalized;
                        results.Add(new TransformData(playerPos, Quaternion.LookRotation(Vector3.forward, direction)));
                    }
                    break;

                case TargetingStyle.RandomCardinal:
                    ShuffleBag<Vector2> directionPicker = new ShuffleBag<Vector2>(WorldSenses.cardinalDirections);
                    Vector2 nextDirection = directionPicker.GetNext();

                    results.Add(new TransformData(playerPos, Quaternion.LookRotation(Vector3.forward, nextDirection)));
                    break;

                case TargetingStyle.RadialBurst:
                    int count = (int)spellStats.GetStat(StatType.Amount).CurrentValue;
                    float angleStep = 360f / count;

                    for (int i = 0; i < count; i++)
                    {
                        float angle = i * angleStep;
                        Quaternion rotation = Quaternion.Euler(0, 0, angle);
                        results.Add(new TransformData(playerPos, rotation));
                    }
                    break;
            }
            return results;
        }
    }

    // Helper struct to keep positions/rotations together
    public struct TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public TransformData(Vector3 p, Quaternion r) { Position = p; Rotation = r; }
    }
}
