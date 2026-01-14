using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    // May implement required components
    // [RequireComponent(typeof(Rigidbody2D))]
    // [RequireComponent(typeof(Collider2D))]

    public abstract class ProjectileGO : SpellGO
    {
        public Rigidbody2D RB { get; private set; }

        protected virtual void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

        protected abstract void Move();
    }
}
