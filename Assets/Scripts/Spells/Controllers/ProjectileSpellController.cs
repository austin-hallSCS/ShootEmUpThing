using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class ProjectileSpellController : SpellController
    {
        //Status variables
        protected float currentProjectileIntervalTimeAt;

        protected override void SpellActiveBehavior()
        {
            var projectileAmount = spellStats.GetStat(StatType.Amount).CurrentValue;

            for (int i = 0; i < projectileAmount; i++)
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

        }

        protected virtual void ResetProjectileIntervalTime()
        {
            currentProjectileIntervalTimeAt = spellStats.ProjectileIntervalTime;
        }
    }
}
