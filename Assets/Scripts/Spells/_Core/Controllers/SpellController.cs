using System;
using UnityEngine;
using WizardGame.Stats;
using WizardGame.Utils;

namespace WizardGame.Spells
{
    public enum TargetingStyle
    {
        None,
        NearestEnemy,
        RandomCardinal,
        RadialBurst
    }
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

        protected Transform GetNearestEnemy()
        {
            Vector2 center = transform.position;
            float circleRadius = 50f;
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(center, circleRadius, whatIsEnemy);

            float closestDistance = Mathf.Infinity;
            Transform nearestTarget = null;
            if (detectedEnemies != null && detectedEnemies.Length > 0)
            {

                foreach (var enemy in detectedEnemies)
                {
                    Vector3 enemyPosition = enemy.transform.position;

                    float distance = WorldSenses.GetSquareDistance(enemyPosition, center);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestTarget = enemy.transform;
                    }
                }
            }

            return nearestTarget;
        }

        protected virtual void ResetCoolDown() => currentCoolDownTimeAt = spellStats.GetStat(StatType.Cooldown).CurrentValue;

        protected virtual void ResetDuration() => currentDurationTimeAt = spellStats.GetStat(StatType.Duration).CurrentValue;
    }
}
