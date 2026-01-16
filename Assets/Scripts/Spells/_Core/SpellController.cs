using System;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public abstract class SpellController : MonoBehaviour
    {
        //-- Inspector Properties --
        [Header("Identity/Data")]
        [SerializeField] protected GameObject spellPrefab;
        [field: SerializeField] public SpellDataSO SpellData { get; private set; }

        [Header("WorldSpace")]
        [SerializeField] protected LayerMask whatIsEnemy;
        [SerializeField] protected float spawnRadius;


        //-- Stats and Abilities --
        protected SpellStats spellStats;
        public SpellStats SpellStats => spellStats;
        protected PlayerAbilities ownerAbilities;

        // Status variables
        protected float currentCoolDownTimeAt;
        protected float coolDownTime;
        protected float currentDurationTimeAt;
        protected bool isActive;


        // Initializes the spell with a reference to the caster's abilities.
        public virtual void Initialize(PlayerAbilities abilities)
        {
            ownerAbilities = abilities;

            InitStats();
        }

        protected virtual void Start()
        {
            SpellDeactivate();
        }

        protected virtual void FixedUpdate()
        {
            CheckSpellActiveStatus();
            if (isActive)
            {
                SpellActiveBehavior();
            }
        }

        public virtual void InitStats()
        {
            if (SpellData == null)
            {
                Debug.LogError($"Spell Data not assigned on: {gameObject.name}");
            }
            spellStats = new SpellStats(SpellData, ownerAbilities);
        }

        public virtual void LevelUp()
        {
            spellStats.ApplyLevelUp();
            // Debug.Log("Spell Leveled up!");
        }

        protected virtual void CheckSpellActiveStatus()
        {
            if (!isActive)
            {
                currentCoolDownTimeAt -= Time.deltaTime;
                if (currentCoolDownTimeAt <= 0)
                {
                    SpellActivate();
                }
            }
        }

        protected virtual void SpellActivate()
        {
            isActive = true;
        }

        protected virtual void SpellDeactivate()
        {
            isActive = false;
            ResetCoolDown();
        }

        protected abstract void SpellActiveBehavior();

        protected virtual void ResetCoolDown() => currentCoolDownTimeAt = spellStats.GetStat(StatType.Cooldown).CurrentValue;

        protected virtual void ResetDuration() => currentDurationTimeAt = spellStats.GetStat(StatType.Duration).CurrentValue;
    }
}
