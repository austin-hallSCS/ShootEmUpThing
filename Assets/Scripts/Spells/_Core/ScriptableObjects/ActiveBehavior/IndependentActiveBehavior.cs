using UnityEngine;

namespace WizardGame.Spells
{
    [CreateAssetMenu(fileName = "IndependentActiveBehavior_Asset", menuName = "Spells/Active Behaviors/Independent")]
    public class IndependentActiveBehavior : ActiveBehaviorSO
    {
        public override void Activate(SpellCastContext context, ISpawnBehavior spawnBehavior)
        {
            spawnBehavior.Execute(context);

            Deactivate();
        }

        public override void Deactivate() { }
    }
}
