using UnityEngine;
using WizardGame.PlayerSystem;

namespace WizardGame.EnvironmentSystem
{
    public class SigilController : MonoBehaviour
    {
        public int DamageAmount;
        void OnTriggerStay2D(Collider2D other)
        {
            PlayerController controller = other.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.Damage(1f);
            }
        }
    }
}
