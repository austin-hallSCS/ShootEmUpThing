using UnityEngine;
using WizardGame.Collectibles;
using WizardGame.Interfaces;
using WizardGame.Managers;
using WizardGame.Player;
using WizardGame.Stats;

namespace WizardGame.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [SerializeField] protected EnemyDataSO enemyData;
        [SerializeField] protected GameObject experiencePrefab;

        // Object references
        private GameManager gameManager;
        private Rigidbody2D playerRB;

        protected EnemyStats enemyStats;

        // Movement variables
        public Rigidbody2D rb { get; private set; }

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
            if (playerRB != null)
            {
                var moveSpeed = enemyStats.GetStat(StatType.MovementSpeed).CurrentValue;
                Vector2 currentPosition = rb.position;
                Vector2 target = playerRB.position;
                Vector2 position = Vector2.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

                rb.MovePosition(position);
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

        public void CheckHealth()
        {
            var healthAmount = enemyStats.GetStat(StatType.Health).CurrentValue;
            if (healthAmount <= 0)
            {
                OnDeath();
            }
        }

        public void OnDeath()
        {
            var prefab = Instantiate(experiencePrefab, transform.position, Quaternion.identity);
            CollectibleExperienceOrb xpOrb = prefab.GetComponent<CollectibleExperienceOrb>();
            int rewardExperience = enemyStats.RewardExperience;

            xpOrb.SetXPAmount(rewardExperience);

            EventManager.PublishEnemyDied(this);

            Destroy(gameObject);
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