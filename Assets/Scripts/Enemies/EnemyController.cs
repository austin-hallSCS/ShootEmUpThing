using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Interfaces;
using WizardGame.PlayerSystem;
using WizardGame.Stats;

namespace WizardGame.EnemySystem
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
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
            enemyStats = GetComponentInChildren<EnemyStats>();
            spawner = GetComponentInParent<EnemySpawner>();
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
            currentPosition = rb.position;
            target = spawner.PlayerRB.position;
            position = Vector2.MoveTowards(currentPosition, target, enemyStats.SpeedAmount.CurrentValue * Time.deltaTime);
            rb.MovePosition(position);

        }

        public void Damage(float amount)
        {
            enemyStats.Health.Decrease(amount);
            CheckHealth();

        }

        public void CheckHealth()
        {
            if (enemyStats.Health.CurrentValue <= 0)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            detectedPlayer = other.gameObject.GetComponent<PlayerController>();

            if (detectedPlayer != null)
            {
                detectedPlayer.Damage(enemyStats.DamageAmount.CurrentValue);
            }
        }
    }
}