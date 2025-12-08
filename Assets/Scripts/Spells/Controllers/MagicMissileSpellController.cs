using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class MagicMissileSpellController : ProjectileSpellController
    {
        private float curveHeightRange = 2.0f;

        protected override void SpellActiveBehavior()
        {
            StartCoroutine(FireBurst());
        }

        private IEnumerator FireBurst()
        {
            var amount = spellStats.GetStat(StatType.Amount).CurrentValue;
            var projectileInterval = spellStats.ProjectileIntervalTime;

            for (int i = 0; i < amount; i++)
            {
                FireProjectile();
                yield return new WaitForSeconds(projectileInterval);
            }

            SpellDeactivate();
        }

        protected override void FireProjectile()
        {
            Debug.Log("Magic Missle fired.");

            var enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

            Transform target = null;
            if (enemies.Length > 0)
            {
                target = enemies[UnityEngine.Random.Range(0, enemies.Length)].transform;
            }

            // Instantiate projectile
            var projectile = Instantiate(spellPrefab, transform.position, quaternion.identity);
            var missileScript = projectile.GetComponent<MagicMissileGO>();

            missileScript.Initialize(spellStats);

            float randomArc = UnityEngine.Random.Range(-curveHeightRange, curveHeightRange);
            missileScript.SetTarget(target, randomArc);
        }
    }
}
