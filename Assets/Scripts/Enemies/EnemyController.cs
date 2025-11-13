using System;
using UnityEngine;
using WizardGame.Interfaces;
using WizardGame.Player;
using WizardGame.Stats;

namespace WizardGame.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [SerializeField] protected EnemyDataSO enemyData;

        // Object references
        private PlayerController detectedPlayer;
        private EnemySpawner spawner;

        protected EnemyStats enemyStats;



        //Status variables
        public int Health { get { return currentHealth; } }

        // Movement variables
        private Rigidbody2D rb;
        private Vector2 target;
        private Vector2 position;

        // Private data variables
        private float movementSpeed;

        // Private status variables
        private int currentHealth;
        private Vector2 currentPosition;

        void Awake()
        {
            // Get Component References
            rb = GetComponent<Rigidbody2D>();
            // enemyStats = GetComponentInChildren<EnemyStats>();
            spawner = GetComponentInParent<EnemySpawner>();

            // Init stats
            if (enemyData == null)
            {
                Debug.LogError($"Enemy Data not assigned on: {gameObject.name}");
            }
            enemyStats = new EnemyStats(enemyData);
        }

        void Start()
        {
            

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            var moveSpeed = enemyStats.GetStat(StatType.MovementSpeed).CurrentValue;
            currentPosition = rb.position;
            target = spawner.PlayerRB.position;
            position = Vector2.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

            rb.MovePosition(position);

        }

        public void Damage(float amount)
        {
            var healthStat = enemyStats.GetStat(StatType.Health);
            float damageResistanceAmount = enemyStats.GetStat(StatType.DamageResistance).CurrentValue;

            float effectiveDamage = Mathf.Max(0, amount - damageResistanceAmount);
            healthStat.Decrease(effectiveDamage);
            Debug.Log($"Enemy health: {healthStat.CurrentValue}");
            CheckHealth();

        }

        public void CheckHealth()
        {
            var healthAmount = enemyStats.GetStat(StatType.Health).CurrentValue;
            if (healthAmount <= 0)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            detectedPlayer = other.gameObject.GetComponent<PlayerController>();

            if (detectedPlayer != null)
            {
                var damageAmount = enemyStats.GetStat(StatType.Damage).CurrentValue;
                detectedPlayer.Damage(damageAmount);
            }
        }
    }
}