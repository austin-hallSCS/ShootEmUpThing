using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Spells;
using WizardGame.Interfaces;
using WizardGame.Stats;
using WizardGame.UI;

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
        protected PlayerStats playerRuntimeStats;
        protected PlayerAbilities playerRuntimeAbilities;


        // Input variables
        public InputAction MoveAction;
        public InputAction AttackAction;
        public InputAction AbilityAction;
        public InputAction DashAction;
        public int NormInputX;

        // Movement variables
        public Rigidbody2D rb { get; private set; }
        private Vector2 position;
        private Vector2 move;
        private Vector2 facingDirection;
        private int facingDirectionx;

        // Player status variables
        private bool isInvincible;
        private float damageCooldown;

        // Other variables
        // private List<SpellController> equippedSpells;

        private void Awake()
        {
            if (playerAbilityData == null)
            {
                Debug.LogError($"Player Ability Data not assigned on: {gameObject.name}");

            }
            if (playerData == null)
            {
                Debug.LogError($"Player Data not assigned on {gameObject.name}");
            }

            playerRuntimeStats = PlayerStats.CopyFrom(playerData);
            Debug.Log($"Player Runtime Stats: {playerRuntimeStats.MovementSpeed.CurrentValue}");
            playerRuntimeAbilities = PlayerAbilities.CopyFrom(playerAbilityData);
            Debug.Log($"Player Runtime Abilities: {playerRuntimeAbilities.Strength.CurrentValue}");

            // Get Component references
            rb = GetComponent<Rigidbody2D>();
            healthbar = GetComponentInChildren<HealthBarController>();
            FireBall = GetComponentInChildren<FireballSpellController>();

            // equippedSpells.Add(transform.Find("FireballSpellController").GetComponent<FireballSpellController>());
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            facingDirection.Set(move.x, move.y);
            NormInputX = Mathf.RoundToInt(facingDirection.x);
            facingDirectionx = Mathf.RoundToInt(facingDirection.x);

            CheckIfShouldFlip();
            

            // Damage cooldown
            if (isInvincible)
            {
                damageCooldown -= Time.deltaTime;
                if (damageCooldown <= 0)
                {
                    isInvincible = false;
                    damageCooldown = 0.25f;
                }
            }
        }

        void FixedUpdate()
        {
            position = rb.position + move * playerRuntimeStats.MovementSpeed.CurrentValue * Time.deltaTime;
            rb.MovePosition(position);
        }

        private void CheckIfShouldFlip()
        {
            if (NormInputX != 0 && NormInputX != facingDirectionx)
            {
                Flip();
            }
        }

        private void Flip()
        {
            facingDirection *= -1;
            rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        public void Damage(float amount)
        {
            if (!isInvincible)
            {
                playerRuntimeStats.Health.Decrease(amount);
                healthbar.UpdateHealthBar(playerRuntimeStats.Health.CurrentValue, playerRuntimeStats.Health.MaxValue);
                Debug.Log(playerRuntimeStats.Health.CurrentValue);
                isInvincible = true;
                CheckHealth();
            }
        }

        private void CheckHealth()
        {
            if (playerRuntimeStats.Health.CurrentValue <= 0)
            {
                // Destroy(gameObject);
                Debug.Log("Player is dead!");
            }
        }
    }
}

