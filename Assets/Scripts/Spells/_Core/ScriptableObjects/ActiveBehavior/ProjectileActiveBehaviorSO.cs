using UnityEngine;
using WizardGame.Services;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "ProjectileActiveBehaviorSO", menuName = "Spells/Active Behaviors/Projectile")]
    public class ProjectileActiveBehaviorSO : ActiveBehaviorSO
    {
        public override void Activate(SpellCastContext context)
        {
            GameObject gameObject = PoolService.Spawn(context.Data.SpellPrefab, context.Caster.position, context.Caster.rotation);

            if (gameObject.TryGetComponent(out SpellGO spellGO))
            {
                spellGO.Initialize(context.Data, context.Stats);
            }

        }
        public override void SpellActiveBehavior()
        {

        }

        public override void Deactivate()
        {

        }
    }
}
