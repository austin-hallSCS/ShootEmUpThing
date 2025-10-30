using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.SpellSystem;

namespace WizardGame.PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        // Component references
        private HealthBarController healthbar;
        public FireballSpellController FireBall { get; private set; }


        // Input variables
        public InputAction MoveAction;
        public InputAction AttackAction;
        public InputAction AbilityAction;
        public InputAction DashAction;
        public int NormInputX;

        // Level up/Bonus variables
        private float damgeBonus;
        private float areaBonus;
        private float speedBonus;
        private float cooldownBonus;
        private float knockbackBonus;
        private float projectileAmountBonus;
        private float durationBonus;
        private float pierceBonus;

        // Player data variables
        public int MaxHealth;
        public float MovementSpeed;
        public float DamageCoolDownTime;

        // Player status variables
        public int Health { get { return currentHealth; } }

        // Movement variables
        public Rigidbody2D rb { get; private set; }
        private Vector2 position;
        private Vector2 move;
        private Vector2 facingDirection;
        private int facingDirectionx;

        // Private player data variables
        private float movementSpeed;

        // Player status variables
        private int currentHealth;
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

            // equippedSpells.Add(transform.Find("FireballSpellController").GetComponent<FireballSpellController>());
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            MoveAction.Enable();

            // Set player values
            currentHealth = MaxHealth;
            movementSpeed = MovementSpeed;
            isInvincible = false;
        }

        // Update is called once per frame
        void Update()
        {
            move = MoveAction.ReadValue<Vector2>();
            facingDirection.Set(move.x, move.y);
            NormInputX = Mathf.RoundToInt(facingDirection.x);
            facingDirectionx = Mathf.RoundToInt(facingDirection.x);

            if (NormInputX != 0 && NormInputX != facingDirectionx)
            {
                Flip();
            }

            // Damage cooldown
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
            position = (Vector2)rb.position + move * movementSpeed * Time.deltaTime;
            rb.MovePosition(position);
        }

        public void Flip()
        {
            facingDirection *= -1;
            rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        public void ChangeHealth(int amount)
        {
            if (amount < 0)
            {
                if (isInvincible)
                {
                    return;
                }
                isInvincible = true;
                damageCooldown = DamageCoolDownTime;
            }
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
            healthbar.UpdateHealthBar(currentHealth, MaxHealth);
        }
    }
}

