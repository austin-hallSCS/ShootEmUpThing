using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;
using WizardGame.Utils;

namespace WizardGame.Spells
{
    public class SpellGO : MonoBehaviour
    {
        protected SpellStats spellStats;

        // Component references
        public Rigidbody2D RB { get; private set; }
        public CircleCollider2D CircleCollider { get; private set; }
        public Animator Animator { get; private set; }

        private float inAirTime = 0.75f;

        private float timeAlive;
        private bool inAir;

        private void Awake()
        {
            // Get components
            RB = GetComponent<Rigidbody2D>();
            CircleCollider = GetComponent<CircleCollider2D>();
            Animator = GetComponent<Animator>();
        }

        public void Initialize(SpellStats stats)
        {
            spellStats = stats;

            AddAreaStat();

            timeAlive = 0f;
            inAir = true;
            
            Animator.SetBool("inAir", inAir);

            Launch();
        }

        private void Start()
        {

        }
        
        private void Update()
        {
            timeAlive += Time.deltaTime;
            if (timeAlive >= inAirTime)
            {
                Explode();
            }
        }

        void FixedUpdate()
        {
            
        }

        public void Launch()
        {
            var speedAmount = spellStats.GetStat(StatType.Speed).CurrentValue;
            RB.linearVelocity = speedAmount * transform.right;
        }

        public void AddAreaStat()
        {
            // Break out of function if stats is null
            if (spellStats == null) return;

            // Get Area Amount current value
            float areaMultiplier = spellStats.GetStat(StatType.Area).CurrentValue;

            // Increase size
            transform.localScale = new Vector3(areaMultiplier, areaMultiplier, 1f);
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

            // Debug.Log($"Damage Amount: {spellStats.DamageAmount.CurrentValue}");

        }

        void AnimationFinishTrigger() => Destroy(gameObject);

        void OnTriggerStay2D(Collider2D other)
        {
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
