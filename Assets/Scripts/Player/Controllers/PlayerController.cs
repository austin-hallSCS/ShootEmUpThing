using UnityEngine;
using WizardGame.Spells;
using WizardGame.Interfaces;
using WizardGame.Stats;
using WizardGame.UI;
using WizardGame.Managers;
using WizardGame.Services;

namespace WizardGame.Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        // Data Scriptable Objects
        [SerializeField] private PlayerAbilityDataSO playerAbilityData;
        [SerializeField] private PlayerDataSO playerData;

        // Component references
        private HealthBarController healthbar;
        // public FireballSpellController FireBall { get; private set; }

        // Stats and Abilities
        protected PlayerStats playerStats;
        public PlayerAbilities PlayerAbilities { get; private set; }


        // Movement variables
        public Rigidbody2D RB { get; private set; }
        private Vector2 move;
        private int currentFacingDirection = 1;


        // Player status variables
        private bool isInvincible;
        private float damageCooldown;

        // Other variables
        // private List<SpellController> equippedSpells;

        #region Unity Callback Functions

        private void Awake()
        {
            GetComponentReferences();
            ValidateData();
            InitStatsAndAbilities();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        void Start()
        {
            // Init player values
            isInvincible = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance != null)
            {
                move = GameManager.Instance.MoveInput;
            }

            CheckIfShouldFlip();


            // Damage cooldown - planning on removing this in favor of an "attack cooldown"
            if (isInvincible)
            {
                damageCooldown -= Time.deltaTime;
                if (damageCooldown <= 0)
                {
                    isInvincible = false;
                }
            }
        }

        void FixedUpdate()
        {
            Move();
        }

        void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        // Gets and stores references to the required components
        private void GetComponentReferences()
        {
            RB = GetComponent<Rigidbody2D>();
            healthbar = GetComponentInChildren<HealthBarController>();
        }

        // Checks that all required ScriptableObject data is assigned
        private void ValidateData()
        {
            if (playerAbilityData == null)
            {
                Debug.LogError($"Player Ability Data not assigned on: {gameObject.name}");

            }
            if (playerData == null)
            {
                Debug.LogError($"Player Data not assigned on {gameObject.name}");
            }
        }

        // Creates runtime instances of stats and abilities
        private void InitStatsAndAbilities()
        {
            PlayerAbilities = new PlayerAbilities(playerAbilityData);
            playerStats = new PlayerStats(playerData, PlayerAbilities);
        }

        private void SubscribeToEvents()
        {
            EventBus.OnPlayerLevelUp += HandlePlayerLevelUp;
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.OnPlayerLevelUp -= HandlePlayerLevelUp;
        }

        #region Runtime Methods
        private void Move()
        {
            var moveSpeed = playerStats.GetStat(StatType.MovementSpeed).CurrentValue;
            Vector2 position = RB.position + move * moveSpeed * Time.fixedDeltaTime;

            RB.MovePosition(position);
        }

        private void CheckIfShouldFlip()
        {
            if (move.x > 0.1f && currentFacingDirection == -1)
            {
                Flip();
            }
            else if (move.x < -0.1f && currentFacingDirection == 1)
            {
                Flip();
            }
        }

        private void Flip()
        {
            currentFacingDirection *= -1;

            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        public void Damage(float amount)
        {
            if (!isInvincible)
            {
                var healthStat = playerStats.GetStat(StatType.Health);

                healthStat.Decrease(amount);
                healthbar.UpdateHealthBar(healthStat.CurrentValue, healthStat.Cap);
                Debug.Log($"Player Health: {healthStat.CurrentValue}");

                // TODO: Remove this, implement attack cooldowns
                isInvincible = true;
                damageCooldown = 0.25f;

                CheckHealth();
            }
        }

        private void CheckHealth()
        {
            var currentHealth = playerStats.GetStat(StatType.Health).CurrentValue;
            if (currentHealth <= 0)
            {
                //TODO: Implement player death
                // Destroy(gameObject);
                Debug.Log("Player is dead!");
            }
        }

        #endregion
        #region Event Handlers

        private void HandlePlayerLevelUp(int newLevel)
        {
            Debug.Log("Player level up!");
            // TODO: Handle level up
        }

        #endregion
    }
}

