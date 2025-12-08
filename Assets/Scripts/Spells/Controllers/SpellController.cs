using System;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public abstract class SpellController : MonoBehaviour
    {
        [SerializeField] protected LayerMask whatIsEnemy;
        [SerializeField] protected GameObject spellPrefab;
        [SerializeField] protected float spawnRadius;
        [SerializeField] protected SpellDataSO spellData;

        protected SpellStats spellStats;
        protected PlayerAbilities ownerAbilities;

        // Timers - need to figure this out later
        // protected Timer levelUpTimer = new Timer(5f);

        // Temp level up timer for testing
        // protected float currentLevelUpTimerAt;


        // Status variables
        protected float currentCoolDownTimeAt;
        protected float coolDownTime;
        protected float duration;
        protected float currentDurationTimeAt;
        protected float projectileAmount;
        protected bool isActive;


        // Initializes the spell with a reference to the caster's abilities.
        public virtual void Initialize(PlayerAbilities abilities)
        {
            ownerAbilities = abilities;

            InitStats();
        }


        protected virtual void Awake() { }

        protected virtual void Start()
        {
            SpellDeactivate();
        }

        protected virtual void Update() { }

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
            if (spellData == null)
            {
                Debug.LogError($"Spell Data not assigned on: {gameObject.name}");
            }
            spellStats = new SpellStats(spellData, ownerAbilities);
        }

        public virtual void LevelUp()
        {
            spellStats.ApplyLevelUp();
            Debug.Log("Spell Leveled up!");
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
