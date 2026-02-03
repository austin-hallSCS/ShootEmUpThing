using UnityEngine;
using WizardGame.Utils;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "NearestEnemy_Asset", menuName = "Spells/Targeting/Nearest Enemy")]
    public class NearestEnemyStrategySO : TargetingStrategySO
    {
        public override SpawnTransform GetSpawnTransform(SpellCastContext context, int index, int totalCount)
        {
            // Get the nearest enemy if the context does not already have one
            if (context.TargetEnemy == null)
            {
                // FIXME: Get enemy mask directly from context once static helper is created
                context.TargetEnemy = EnemyFinder.GetNearest(context.Caster, context.Controller.WhatIsEnemy);
            }

            // Default value if TargetEnemy is null
            Vector3 direction = context.Caster.forward;

            if (context.TargetEnemy != null)
            {
                Vector3 diff = context.TargetEnemy.position - context.Caster.position;

                // Divide by 0 check
                if (diff.sqrMagnitude > Mathf.Epsilon)
                {
                    direction = diff.normalized;
                }
            }

            return new SpawnTransform
            {
                Position = context.Caster.position,
                Rotation = Quaternion.LookRotation(direction)
            };
        }
    }
}
