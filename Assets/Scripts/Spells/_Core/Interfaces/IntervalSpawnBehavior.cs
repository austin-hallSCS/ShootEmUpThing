using UnityEngine;

namespace WizardGame.Spells
{
    public class IntervalSpawnBehavior : ISpawnBehavior
    {
        public void Spawn()
        {
            Fireburst();
        }

        protected virtual void Fireburst() { }
    }
}
