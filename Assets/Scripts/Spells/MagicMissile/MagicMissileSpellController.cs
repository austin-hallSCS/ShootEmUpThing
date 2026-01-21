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

            missileScript.Initialize(this, target);
        }


    }
}
