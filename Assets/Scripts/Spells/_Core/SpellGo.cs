using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public abstract class SpellGO : MonoBehaviour
    {
        protected SpellStats spellStats;

        public Rigidbody2D RB { get; private set; }

        protected float areaMultiplier = 1.0f;

        public virtual void Initialize(SpellStats stats)
        {
            spellStats = stats;
            CalculateStats();
        }

        protected virtual void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
        }

        protected virtual void CalculateStats()
        {
            if (spellStats == null) return;

            areaMultiplier = spellStats.GetStat(StatType.Area).CurrentValue;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other) { }

        public void DestroySelf() => Destroy(gameObject);

        void AnimationFinishTrigger() => Destroy(gameObject);
    }
}
