using System.Collections;
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

        //-- Stats and Abilities --
        public SpellDataSO SpellData { get; private set; }

        // FIXME: Change spellStats to a field rather than 2 properties
        protected SpellStats spellStats;
        public SpellStats SpellStats => spellStats;
        protected PlayerAbilities ownerAbilities;

        //-- Behavior --
        private ActiveBehaviorSO activeBehavior;

        //-- Spawning --
        private ISpawnBehavior spawnBehavior;

        //-- Context --
        private SpellCastContext activeContext;

        //-- Worldspace --
        private LayerMask whatIsEnemy;

        // Status variables
        protected float currentCoolDownTimeAt;
        protected float coolDownTime;
        protected float currentDurationTimeAt;
        protected bool isActive;

        #region Initialization

        // Initializes the spell with a reference to the caster's abilities.
        public virtual void Initialize(SpellDataSO data, PlayerAbilities abilities)
        {
            SpellData = data;
            ownerAbilities = abilities;
            activeBehavior = SpellData.ActiveBehavior;

            // FIXME: Create static helper to hold references to Layers in order to save on memory
            whatIsEnemy = LayerMask.GetMask("Enemies");

            InitStats();
        }

        public virtual void InitStats()
        {
            if (SpellData == null)
            {
                Debug.LogError($"Spell Data not assigned on: {gameObject.name}");
            }
            spellStats = new SpellStats(SpellData, ownerAbilities);
        }

        #endregion

        #region Unity Callback Functions

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

        #endregion

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
                    CastSpell();
                }
            }
        }

        private IEnumerator CastSpell()
        {
            spawnBehavior = new IntervalSpawnBehavior();
            activeContext = new SpellCastContext(transform, this, spellStats, SpellData);

            yield return activeBehavior.Activate(activeContext, spawnBehavior);
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

                    float distance = enemy.transform.GetSquareDistance(transform);

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
