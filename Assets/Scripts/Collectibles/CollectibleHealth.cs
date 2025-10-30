using UnityEngine;
using WizardGame.PlayerSystem;

namespace WizardGame.Collectibles
{
    public class CollectibleHealth : MonoBehaviour
    {
        // Object references
        private PlayerController player;

        // Data
        public int HealAmount;

        void OnTriggerEnter2D(Collider2D other)
        {
            player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ChangeHealth(HealAmount);
                Destroy(gameObject);
            }
        }
    }
}
