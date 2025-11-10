using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.SpellSystem;
using WizardGame.Interfaces;
using WizardGame.Stats;

namespace WizardGame.PlayerSystem
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        // Component references
        private HealthBarController healthbar;
        public FireballSpellController FireBall { get; private set; }
        protected PlayerStats playerStats;


        // Input variables
        public InputAction MoveAction;
        public InputAction AttackAction;
        public InputAction AbilityAction;
        public InputAction DashAction;
        public int NormInputX;


        // Player data variables
        public int MaxHealth;
        public float MovementSpeed;
        public float DamageCoolDownTime;

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
            // Get Component references
            rb = GetComponent<Rigidbody2D>();
            healthbar = GetComponentInChildren<HealthBarController>();
            FireBall = GetComponentInChildren<FireballSpellController>();
            playerStats = GetComponentInChildren<PlayerStats>();

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
            position = (Vector2)rb.position + move * playerStats.MovementSpeed * Time.deltaTime;
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
                playerStats.Health.Decrease(amount);
                healthbar.UpdateHealthBar(playerStats.Health.CurrentValue, playerStats.Health.MaxValue);
                Debug.Log(playerStats.Health.CurrentValue);
                isInvincible = true;
                CheckHealth();
            }
        }

        private void CheckHealth()
        {
            if (playerStats.Health.CurrentValue <= 0)
            {
                // Destroy(gameObject);
                Debug.Log("Player is dead!");
            }
        }
    }
}

