using UnityEngine;

namespace WizardGame.EnemySystem
{
    public class EnemySpawner : MonoBehaviour
    {
        // Object references
        public GameObject Player;
        public GameObject[] EnemyPrefabs;

        // Component references
        public Rigidbody2D PlayerRB { get; private set; }

        // Data variables
        public float SpawnDelayTime;

        // Status variables
        private float timer;

        void Awake()
        {
            // Get component References
            PlayerRB = Player.GetComponent<Rigidbody2D>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ResetTimer();
        }

        // Update is called once per frame
        public virtual void Update()
        {
            timer += Time.deltaTime;

            if (timer >= SpawnDelayTime)
            {
                Instantiate(EnemyPrefabs[0], transform);

                ResetTimer();
            }

        }

        private void ResetTimer()
        {
            timer = 0.0f;
        }
    }
}