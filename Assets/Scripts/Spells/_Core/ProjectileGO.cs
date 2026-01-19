using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    // May implement required components
    // [RequireComponent(typeof(Rigidbody2D))]
    // [RequireComponent(typeof(Collider2D))]

    public abstract class ProjectileGO : SpellGO
    {


        protected override void Awake()
        {

        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

        protected abstract void Move();
    }
}
