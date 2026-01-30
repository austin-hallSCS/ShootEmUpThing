using UnityEngine;
using WizardGame.Services;

namespace WizardGame.Spells
{
    public class ProjectileActiveBehavior : IActiveBehavior
    {
        public void Activate(SpellCastContext context)
        {
            GameObject gameObject = PoolService.Spawn(context.Data.SpellPrefab, context.Caster.position, context.Caster.rotation);

            if (gameObject.TryGetComponent(out SpellGO spellGO))
            {
                spellGO.Initialize(context.Data, context.Stats);
            }

        }
        public void SpellActiveBehavior()
        {

        }

        public void Deactivate()
        {

        }
    }
}
