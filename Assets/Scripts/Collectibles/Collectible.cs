using UnityEngine;
using WizardGame.Player;

namespace WizardGame.Collectibles
{
    public class Collectible : MonoBehaviour
    {
        // Object references
        protected PlayerController player;

        void OnTriggerEnter2D(Collider2D other)
        {
            player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                OnCollected();
                Destroy(gameObject);
            }
        }

        public virtual void OnCollected()
        {
            
        }
    }
}
