using UnityEngine;
using WizardGame.Player;

namespace WizardGame.Collectibles
{
    public class Collectible : MonoBehaviour
    {
        // Object references
        protected PlayerController player;

        // Status variables
        private bool isMagentized = false;

        // Movement
        private float moveSpeed = 5f;
        private float acceleration = 15f;

        protected virtual void Update()
        {
            if (isMagentized && player != null)
            {
                MoveTowardPlayer();
            }
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                OnCollected();
                Destroy(gameObject);
                return;
            }

            if (other.GetComponent<PlayerMagnet>() != null)
            {
                Debug.Log("Hit PlayerMaget.");
                player = other.GetComponentInParent<PlayerController>();
                isMagentized = true;
            }
        }

        private void MoveTowardPlayer()
        {
            moveSpeed += acceleration * Time.deltaTime;

            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );
        }

        public virtual void OnCollected() { }
    }
}
