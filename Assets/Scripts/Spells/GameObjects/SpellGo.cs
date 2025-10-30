using UnityEngine;
using WizardGame.EnemySystem;
using WizardGame.Stats;
using WizardGame.Utils;

namespace WizardGame.SpellSystem
{
    public class SpellGO : MonoBehaviour
    {
        [SerializeField] protected SpellDataSO spellData;
        protected SpellStats spellStats;

        // Component references
        public Rigidbody2D RB { get; private set; }
        public CircleCollider2D CircleCollider { get; private set; }
        public Animator Animator { get; private set; }

        private float inAirTime = 0.5f;
        private float scaleMult = 0.0f;

        private Vector2 direction;
        private float rotation;
        private float timeAlive;
        private bool inAir;

        private void Awake()
        {
            // Get components
            RB = GetComponent<Rigidbody2D>();
            CircleCollider = GetComponent<CircleCollider2D>();
            Animator = GetComponent<Animator>();
            spellStats = transform.parent.GetComponentInChildren<SpellStats>();

            // Init variables
            timeAlive = 0.0f;
            inAir = true;
        }

        private void Start()
        {
            direction = WorldSenses.GetRandomDirection();
            rotation = WorldSenses.VectorDirectionToCardinalRotation(direction);
            RB.rotation = rotation;
            Launch();

            Animator.SetBool("inAir", inAir);
        }

        void FixedUpdate()
        {
            timeAlive += Time.deltaTime;
            if (timeAlive >= inAirTime)
            {
                Explode();
            }
        }

        // protected float GetRandomCardinalRotation()
        // {
        //     float[] degrees = { 0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f };
        //     int randIndex = Random.Range(0, degrees.Length);
        //     return degrees[randIndex];
        // }

        public void Launch()
        {
            RB.linearVelocity = spellStats.SpeedAmount.CurrentValue * direction;
        }

        public void AddDamageStat(float damageMult)
        {
            
        }

        public void AddAreaStat(float scaleMult)
        {
            transform.localScale += new Vector3(scaleMult, scaleMult, 0.0f);
        }

        private void Explode()
        {
            // Stop movement
            RB.linearVelocity = Vector3.zero;
            
            // Set CircleCollider size and position to same as explosion
            CircleCollider.radius = transform.localScale.x / 4;
            CircleCollider.offset = Vector2.zero;

            // Send boolean to Animator
            inAir = false;
            Animator.SetBool("inAir", inAir);

        }

        void AnimationFinishTrigger() => Destroy(gameObject);

        void OnTriggerStay2D(Collider2D other)
        {
            if (!inAir)
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    Debug.Log(other);
                    enemy.Kill();
                }
            }
        }
    }
}
