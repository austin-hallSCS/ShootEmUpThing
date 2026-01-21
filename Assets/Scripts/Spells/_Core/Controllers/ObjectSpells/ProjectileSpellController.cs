using System.Collections;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public abstract class ProjectileSpellController : ObjectSpellController
    {
        protected override void SpellActivate()
        {
            base.SpellActivate();

            StartCoroutine(FireBurst());
        }

        private IEnumerator FireBurst()
        {
            var amount = spellStats.GetStat(StatType.Amount).CurrentValue;
            var projectileInterval = spellStats.ProjectileIntervalTime;

            for (int i = 0; i < amount; i++)
            {
                FireProjectile();

                if (projectileInterval > 0) yield return new WaitForSeconds(projectileInterval);
            }

            SpellDeactivate();
        }

        protected abstract void FireProjectile();
    }
}
