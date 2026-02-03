using UnityEngine;

namespace WizardGame.Utils
{
    public static class EnemyFinder
    {
        public static Transform GetNearest(Transform source, LayerMask whatIsEnemy)
        {
            Vector2 center = source.position;
            // TODO: Magic Number
            float circleRadius = 50f;

            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(center, circleRadius, whatIsEnemy);

            float closestDistance = Mathf.Infinity;
            Transform nearestTarget = null;
            if (detectedEnemies != null && detectedEnemies.Length > 0)
            {
                foreach (var enemy in detectedEnemies)
                {
                    Vector3 enemyPosition = enemy.transform.position;
                    float distance = enemy.transform.GetSquareDistance(source);

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
