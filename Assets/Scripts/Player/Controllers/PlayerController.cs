using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Spells;
using WizardGame.Interfaces;
using WizardGame.Stats;
using WizardGame.UI;
using WizardGame.Managers;

namespace WizardGame.Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        // Data Scriptable Objects
        [SerializeField] private PlayerAbilityDataSO playerAbilityData;
        [SerializeField] private PlayerDataSO playerData;

        // Component references
        private HealthBarController healthbar;
        public FireballSpellController FireBall { get; private set; }

        // Stats and Abilities
        protected PlayerStats playerStats;
        protected PlayerAbilities playerAbilities;


        // Input variables
        public InputAction MoveAction;
        public int NormInputX;

        // Movement variables
        public Rigidbody2D rb { get; private set; }
        private Vector2 move;
        private int currentFacingDirection = 1;


        // Player status variables
        private bool isInvincible;
        private float damageCooldown;

        // Other variables
        // private List<SpellController> equippedSpells;

        private void Awake()
        {
            GetComponentReferences();
            ValidateData();
            SubscribeToEvents();
            InitStatsAndAbilities();
            InitSpells();
        }

        void Start()
        {
            MoveAction.Enable();

            // Init player values
            isInvincible = false;
        }

        // Update is called once per frame
        void Update()
        {
            move = MoveAction.ReadValue<Vector2>();

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

        // Gets and stores references to the required components
        private void GetComponentReferences()
        {
            rb = GetComponent<Rigidbody2D>();
            healthbar = GetComponentInChildren<HealthBarController>();
            FireBall = GetComponentInChildren<FireballSpellController>();
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

        private void SubscribeToEvents()
        {
            GameManager.Instance.XPManager.OnPlayerLevelUp += HandlePlayerLevelUp;
        }

        // Creates runtime instances of stats and abilities
        private void InitStatsAndAbilities()
        {
            playerAbilities = new PlayerAbilities(playerAbilityData);
            playerStats = new PlayerStats(playerData, playerAbilities);
        }

        private void InitSpells()
        {
            if (FireBall != null)
            {
                FireBall.Initialize(playerAbilities);
            }

            // Need to re-implement this function when there are multiple spells
            // foreach (SpellController spell in equippedSpells)
            // {
            //     spell.Initialize(playerAbilities);
            // }
        }

        
        
        private void Move()
        {
            var moveSpeed = playerStats.GetStat(StatType.MovementSpeed).CurrentValue;
            Vector2 position = rb.position + move * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(position);
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

            rb.transform.Rotate(0.0f, 180.0f, 0.0f);
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

#region Event Handlers
        
        private void HandlePlayerLevelUp()
        {
            Debug.Log("Player level up!");
            // TODO: Handle level up
        }

#endregion
    }
}

