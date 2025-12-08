using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public abstract class SpellGO : MonoBehaviour
    {
        protected SpellStats spellStats;

        // Component references
        public Rigidbody2D RB { get; private set; }
        public CircleCollider2D CircleCollider { get; private set; }

        protected Transform target;

        protected float areaMultiplier = 1.0f;

        protected virtual void Awake()
        {
            // Get components
            RB = GetComponent<Rigidbody2D>();
            CircleCollider = GetComponent<CircleCollider2D>();
        }

        public virtual void Initialize(SpellStats stats)
        {
            spellStats = stats;
            CalculateStats();
        }

        public virtual void Initialize(SpellStats stats, Transform targetTransform)
        {
            spellStats = stats;
            CalculateStats();

            target = targetTransform;
        }

        protected virtual void Start() { }

        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }

        public virtual void Launch() { }

        protected virtual void CalculateStats()
        {
            if (spellStats == null) return;

            areaMultiplier = spellStats.GetStat(StatType.Area).CurrentValue;
        }

        void AnimationFinishTrigger() => Destroy(gameObject);
    }
}
