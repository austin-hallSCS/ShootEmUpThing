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

        protected virtual void Awake()
        {
            // Get components
            RB = GetComponent<Rigidbody2D>();
            CircleCollider = GetComponent<CircleCollider2D>();
        }

        public virtual void Initialize(SpellStats stats)
        {
            spellStats = stats;
            AddAreaStat();
        }

        public virtual void Initialize(SpellStats stats, Transform targetTransform)
        {
            spellStats = stats;
            AddAreaStat();

            target = targetTransform;
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        public virtual void Launch()
        {

        }

        protected virtual void AddAreaStat()
        {
            // Break out of function if stats is null
            if (spellStats == null) return;

            // Get Area Amount current value
            float areaMultiplier = spellStats.GetStat(StatType.Area).CurrentValue;

            // Increase size
            transform.localScale = new Vector3(areaMultiplier, areaMultiplier, 1f);
        }

        void AnimationFinishTrigger() => Destroy(gameObject);
    }
}
