using UnityEngine;

namespace WizardGame.Interfaces
{
    public interface IKnockbackable
    {
        void Knockback(float amount, Vector2 source);
    }
}
