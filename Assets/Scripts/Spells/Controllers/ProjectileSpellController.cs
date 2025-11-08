using UnityEngine;

namespace WizardGame.SpellSystem
{
    public class ProjectileSpellController : SpellController
    {
        //Status variables
        protected float currentProjectileIntervalTimeAt;

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void SpellActiveBehavior()
        {
            base.SpellActiveBehavior();
            for (int i = 0; i < runtimeStats.ProjectileAmount.CurrentValue; i++)
            {
                FireProjectile();
            }
            SpellDeactivate();
        }

        // protected virtual IEnumerator ProjectileChain()
        // {
        //     for (int i = 0; i < runtimeStats.ProjectileAmount.CurrentValue; i++)
        //     {
        //         Debug.Log($"Projectile {i}");
        //         FireProjectile();
        //         yield return new WaitForFixedUpdate();
        //     }
        // } 

        protected virtual void FireProjectile()
        {
            var projectileInst = Instantiate(spellPrefab, (Vector3)transform.position, Quaternion.identity, transform);
        }
        
        protected virtual void ResetProjectileIntervalTime()
        {
            currentProjectileIntervalTimeAt = runtimeStats.ProjectileIntervalTime;
        }
    }
}
