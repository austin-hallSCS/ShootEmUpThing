using System.Collections;
using UnityEngine;
using WizardGame.Player;
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
    public class SpellController : MonoBehaviour
    {
        //-- Stats and Abilities --
        public SpellDataSO SpellData { get; private set; }
        public SpellStats SpellStats { get; private set; }
        protected PlayerAbilities ownerAbilities;

        //-- Behavior --
        private ActiveBehaviorSO activeBehavior;
        private ISpellEmitter spellEmitter;

        //-- Context --
        private SpellCastContext activeContext;

        //-- Worldspace --
        public LayerMask WhatIsEnemy;

        // Status variables
        // TODO: Create TimerService to remove timing logic from individual controllers
        protected float currentCoolDownTimeAt;
        protected float coolDownTime;
        protected float currentDurationTimeAt;

        // TODO: Remove any isActive logic from SpellController. Activation should be handled by activeBehavior
        protected bool isActive;

        #region Initialization

        // Initializes the spell with a reference to the caster's abilities.
        public virtual void Initialize(SpellDataSO data, PlayerController player)
        {
            transform.SetParent(player.transform);

            SpellData = data;
            ownerAbilities = player.PlayerAbilities;
            activeBehavior = SpellData.ActiveBehavior;

            // FIXME: Create static helper to hold references to Layers in order to save on memory
            WhatIsEnemy = LayerMask.GetMask("Enemies");

            InitStats();
        }

        public virtual void InitStats()
        {
            if (SpellData == null)
            {
                Debug.LogError($"Spell Data not assigned on: {gameObject.name}");
            }
            SpellStats = new SpellStats(SpellData, ownerAbilities);
        }

        #endregion

        #region Unity Callback Functions

        protected virtual void Start()
        {
            // SpellDeactivate();
        }

        protected virtual void FixedUpdate()
        {
            CheckSpellActiveStatus();
        }

        #endregion

        public virtual void LevelUp()
        {
            SpellStats.ApplyLevelUp();
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
            spellEmitter = new SpellEmitter();
            activeContext = new SpellCastContext(transform, this, SpellStats, SpellData);

            yield return activeBehavior.Activate(activeContext, spellEmitter);
        }

        protected virtual void SpellDeactivate()
        {
            isActive = false;
            ResetCoolDown();
        }

        protected Transform GetNearestEnemy()
        {
            Vector2 center = transform.position;
            float circleRadius = 50f;
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(center, circleRadius, WhatIsEnemy);

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

        protected virtual void ResetCoolDown() => currentCoolDownTimeAt = SpellStats.GetStat(StatType.Cooldown).CurrentValue;

        protected virtual void ResetDuration() => currentDurationTimeAt = SpellStats.GetStat(StatType.Duration).CurrentValue;
    }
}
