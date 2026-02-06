using System.Collections;
using UnityEngine;
using WizardGame.Collectibles;
using WizardGame.Interfaces;
using WizardGame.Managers;
using WizardGame.Player;
using WizardGame.Spells;
using WizardGame.Stats;
using WizardGame.Services;

namespace WizardGame.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable, IKnockbackable
    {
        [SerializeField] protected EnemyDataSO enemyData;
        [SerializeField] protected GameObject experiencePrefab;

        // Object references
        private GameManager gameManager;
        private Rigidbody2D playerRB;

        protected EnemyStats enemyStats;

        // Movement variables
        public Rigidbody2D rb { get; private set; }

        private float killDistance = 20f;
        private bool stunned;
        private float stunDuration = 0.2f;

        void Awake()
        {
            GetComponentReferences();
            InitStats();
        }

        void OnEnable()
        {
            DependencyLookups();
        }

        void FixedUpdate()
        {
            Move();
            CheckForKillDistance();
        }

        private void GetComponentReferences()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void InitStats()
        {
            if (enemyData == null)
            {
                Debug.LogError($"Enemy Data not assigned on: {gameObject.name}");
            }
            enemyStats = new EnemyStats(enemyData);

            stunned = false;
        }

        private void DependencyLookups()
        {
            gameManager = GameManager.Instance;
            if (gameManager == null || gameManager.PlayerController == null)
            {
                Debug.LogError("PlayerController not registered with GameManager");
                return;
            }

            playerRB = gameManager.PlayerController.RB;
            if (playerRB == null)
            {
                Debug.LogError("Player RigidBody2D is null!");
            }
        }

        private void Move()
        {
            if (playerRB != null && stunned == false)
            {
                var moveSpeed = enemyStats.GetStat(StatType.MovementSpeed).CurrentValue;
                Vector2 currentPosition = rb.position;
                Vector2 target = playerRB.position;

                Vector2 position = Vector2.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

                rb.MovePosition(position);
            }
        }

        private void CheckForKillDistance()
        {
            if (playerRB != null)
            {
                // Compare squares of distance values to avoid expensive square root operations used by Vector2.Distance
                float sqrDistance = (playerRB.position - rb.position).sqrMagnitude;

                if (sqrDistance >= (killDistance * killDistance))
                {
                    EventBus.PublishEnemyDespawn(gameObject);
                    Debug.Log("PublishEnemyDespawn called.");
                }
            }
        }

        // Intakes a spell's Effect Payload and applies all effects
        public void ApplyEffect(SpellEffectPayload payload)
        {
            // Apply damage if present
            if (payload.DamageAmount > 0)
            {
                Damage(payload.DamageAmount);
            }

            // Apply knockback if present
            if (payload.KnockbackAmount > 0)
            {
                Knockback(payload.KnockbackAmount, payload.SourcePosition);
            }

            // Apply status effects
            Debug.Log($"Applying status effect {payload.StatusEffect} to {transform.name}");
            switch (payload.StatusEffect)
            {
                case StatusEffectType.Burn:
                    float burnDamage = payload.DamageAmount * .10f;
                    StartCoroutine(ApplyBurn(payload.StatusDuration, burnDamage));
                    break;
                case StatusEffectType.Freeze:
                    StartCoroutine(ApplyFreeze(payload.StatusDuration));
                    break;
                // TODO: Handle other status effects
                default:
                    break;
            }
        }

        public void Damage(float amount)
        {
            var healthStat = enemyStats.GetStat(StatType.Health);
            float damageResistanceAmount = enemyStats.GetStat(StatType.DamageResistance).CurrentValue;

            float effectiveDamage = Mathf.Max(0, amount - damageResistanceAmount);
            healthStat.Decrease(effectiveDamage);
            CheckHealth();

        }

        public void Knockback(float amount, Vector2 source)
        {
            if (source != null)
            {
                stunned = true;

                Vector2 currentPosition = rb.position;
                Vector2 pushDirection = (currentPosition - source).normalized;

                rb.linearVelocity = Vector2.zero;

                Debug.Log($"Knockback amount: {amount}");
                rb.AddForce(pushDirection * amount, ForceMode2D.Impulse);

                Debug.Log("Enemy Knockback was called.");

                StopAllCoroutines();
                StartCoroutine(ResetStun());
            }
        }

        // --- Coroutines ---
        private IEnumerator ApplyBurn(float duration, float damageAmount)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                Damage(damageAmount);

                // FIXME: Make this not a magic number
                yield return new WaitForSeconds(0.25f);

                elapsedTime += Time.deltaTime;
            }
        }

        private IEnumerator ApplyFreeze(float duration)
        {
            float baseSpeed = enemyStats.GetStat(StatType.MovementSpeed).BaseValue;

            enemyStats.GetStat(StatType.MovementSpeed).SetCurrentValue(0);

            yield return new WaitForSeconds(duration);

            enemyStats.GetStat(StatType.MovementSpeed).SetCurrentValue(baseSpeed);
        }

        private IEnumerator ResetStun()
        {
            yield return new WaitForSeconds(stunDuration);

            stunned = false;
        }

        private void CheckHealth()
        {
            var healthAmount = enemyStats.GetStat(StatType.Health).CurrentValue;
            if (healthAmount <= 0)
            {
                OnDeath();
            }
        }

        private void OnDeath()
        {
            var prefab = Instantiate(experiencePrefab, transform.position, Quaternion.identity);
            CollectibleExperienceOrb xpOrb = prefab.GetComponent<CollectibleExperienceOrb>();
            int rewardExperience = enemyStats.RewardExperience;

            xpOrb.SetXPAmount(rewardExperience);

            EventBus.PublishEnemyDied(this);

            PoolService.Despawn(gameObject, enemyData.EnemyPrefab);
            EventBus.PublishEnemyDespawn(gameObject);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            PlayerController detectedPlayer = other.gameObject.GetComponent<PlayerController>();

            if (detectedPlayer != null)
            {
                var damageAmount = enemyStats.GetStat(StatType.Damage).CurrentValue;
                detectedPlayer.Damage(damageAmount);
            }
        }
    }
}