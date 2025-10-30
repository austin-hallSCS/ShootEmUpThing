using UnityEditor;
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

        protected override void SpellActiveBehavior()
        {
            base.SpellActiveBehavior();

            for (int i = 0; i < spellStats.ProjectileAmount.CurrentValue; i++)
            {
                FireProjectile();
            }
            SpellDeactivate();
        }
        protected virtual void FireProjectile()
        {
            Instantiate(spellPrefab, (Vector3)transform.position, Quaternion.identity, transform);
        }
        
        protected virtual void ResetProjectileIntervalTime()
        {
            currentProjectileIntervalTimeAt = spellStats.ProjectileIntervalTime.CurrentValue;
        }
    }
}
