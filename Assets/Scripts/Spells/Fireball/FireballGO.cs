using System.Collections.Generic;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class FireballGO : ProjectileGO
    {
        //-- Components --
        public Animator Animator { get; private set; }

        private CircleCollider2D circleCollider;

        private float inAirTime = 0.75f;
        private float timeAlive;
        private bool inAir;

        private List<int> damagedEnemyIDs = new List<int>();

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
            circleCollider = GetComponent<CircleCollider2D>();
        }

        public override void Initialize(SpellStats stats)
        {
            base.Initialize(stats);

            timeAlive = 0f;
            inAir = true;

            Launch();
            Animator.SetBool("inAir", inAir);
        }

        protected void Update()
        {
            timeAlive += Time.deltaTime;
            if (timeAlive >= inAirTime)
            {
                Explode();
            }
        }

        protected override void Move()
        {

        }

        public void Launch()
        {
            var speedAmount = spellStats.GetStat(StatType.Speed).CurrentValue;
            RB.linearVelocity = speedAmount * transform.right;
        }

        private void Explode()
        {
            inAir = false;
            Animator.SetBool("inAir", inAir);
            RB.linearVelocity = Vector3.zero;

            transform.localScale = new Vector3(areaMultiplier, areaMultiplier, 1f);

            // Set CircleCollider size and position to same as explosion
            circleCollider.radius = 0.5f;
            circleCollider.offset = Vector2.zero;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (inAir && enemy != null)
            {
                Explode();
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (!inAir)
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    int enemyID = enemy.GetInstanceID();
                    if (damagedEnemyIDs.Contains(enemyID))
                    {
                        return;
                    }
                    else
                    {
                        var damageAmount = spellStats.GetStat(StatType.Damage).CurrentValue;
                        enemy.Damage(damageAmount);
                        damagedEnemyIDs.Add(enemyID);
                    }
                }
            }
        }
    }
}
