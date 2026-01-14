using Unity.Mathematics;
using UnityEngine;
using WizardGame.Utils;

namespace WizardGame.Spells
{
    public class MagicMissileSpellController : ProjectileSpellController
    {
        protected override void FireProjectile()
        {
            Transform target = GetNearestEnemy();

            // Instantiate projectile
            var projectile = Instantiate(spellPrefab, transform.position, quaternion.identity);
            var missileScript = projectile.GetComponent<MagicMissileGO>();

            missileScript.Initialize(spellStats, target);
        }

        private Transform GetNearestEnemy()
        {
            Vector2 center = transform.position;
            float circleRadius = 50f;
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(center, circleRadius, whatIsEnemy);

            float closestDistance = Mathf.Infinity;
            Transform nearestTarget = null;
            if (detectedEnemies != null && detectedEnemies.Length > 0)
            {

                foreach (var enemy in detectedEnemies)
                {
                    Vector3 enemyPosition = enemy.transform.position;

                    float distance = WorldSenses.GetSquareDistance(enemyPosition, center);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestTarget = enemy.transform;
                    }
                }
            }

            return nearestTarget;
        }
    }
}
