using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class FireballGO : SpellGO
    {
        //-- Components --
        public Animator Animator { get; private set; }

        private float inAirTime = 0.75f;
        private float timeAlive;
        private bool inAir;

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
        }

        public override void Initialize(SpellStats stats)
        {
            base.Initialize(stats);

            timeAlive = 0f;
            inAir = true;

            Launch();
            Animator.SetBool("inAir", inAir);
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
            timeAlive += Time.deltaTime;
            if (timeAlive >= inAirTime)
            {
                Explode();
            }
        }

        public override void Launch()
        {
            var speedAmount = spellStats.GetStat(StatType.Speed).CurrentValue;
            RB.linearVelocity = speedAmount * transform.right;
        }

        private void Explode()
        {
            // Send boolean to Animator
            inAir = false;
            Animator.SetBool("inAir", inAir);

            // Stop movement
            RB.linearVelocity = Vector3.zero;

            // Set CircleCollider size and position to same as explosion
            CircleCollider.radius = transform.localScale.x / 4;
            CircleCollider.offset = Vector2.zero;
        }

        void OnTriggerStay2D(Collider2D other)
        {
            // FIXME: Damages enemies on every frame (because of OnTriggerStay)
            if (!inAir)
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    var damageAmount = spellStats.GetStat(StatType.Damage).CurrentValue;
                    enemy.Damage(damageAmount);
                }
            }
        }
    }
}
