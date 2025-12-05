using UnityEngine;

namespace WizardGame.Player
{
    public class PlayerMagnet : MonoBehaviour
    {
        private CircleCollider2D circleCollider;

        public void Awake()
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }

        public void SetNewRadius(float amount)
        {
            float newRadius = circleCollider.radius + amount;

            circleCollider.radius = newRadius;
        }

        public void ResetRadius()
        {
            circleCollider.radius = 1;
        }
    }
}
