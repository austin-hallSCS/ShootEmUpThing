using UnityEngine;
using WizardGame.PlayerSystem;

namespace WizardGame.EnemySystem
{
    public class EnemyController : MonoBehaviour
    {
        // Object references
        private PlayerController detectedPlayer;
        private EnemySpawner spawner;

        // Data variables
        public int MaxHealth;
        public float MovementSpeed;

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
            spawner = GetComponentInParent<EnemySpawner>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // Get Component References
            rb = GetComponent<Rigidbody2D>();

            // Set variables
            currentHealth = MaxHealth;
            movementSpeed = MovementSpeed;

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            currentPosition = rb.position;
            target = spawner.PlayerRB.position;
            position = Vector2.MoveTowards(currentPosition, target, movementSpeed * Time.deltaTime);
            rb.MovePosition(position);

        }

        public void Kill()
        {
            Debug.Log("Enemy killed");
            Destroy(gameObject);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            detectedPlayer = other.gameObject.GetComponent<PlayerController>();

            if (detectedPlayer != null)
            {
                detectedPlayer.ChangeHealth(-1);
            }
        }
    }
}