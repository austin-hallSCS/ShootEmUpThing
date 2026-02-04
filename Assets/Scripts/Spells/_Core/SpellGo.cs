using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public abstract class SpellGO : MonoBehaviour
    {
        protected SpellDataSO spellData;
        protected SpellStats spellStats;

        public Rigidbody2D RB { get; private set; }

        protected float areaMultiplier = 1.0f;

        // TODO: Refactor to take in SpellCastContext instead of data and stats
        public virtual void Initialize(SpellCastContext context)
        {
            spellData = context.Data;
            spellStats = context.Stats;
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

        protected virtual void SendPayload(EnemyController other)
        {
            if (other != null)
            {
                SpellEffectPayload payload = BuildPayload();
                other.ApplyEffect(payload);
            }
        }

        protected virtual SpellEffectPayload BuildPayload()
        {
            return new SpellEffectPayload
            {
                DamageAmount = spellStats.GetStat(StatType.Damage).CurrentValue,
                KnockbackAmount = spellStats.GetStat(StatType.Knockback).CurrentValue,
                SourcePosition = transform.position,
                StatusEffect = spellData.Payload.StatusEffect,
                StatusDuration = spellData.Payload.StatusDuration
            };
        }

        public void DestroySelf() => Destroy(gameObject);

        void AnimationFinishTrigger() => Destroy(gameObject);
    }
}
