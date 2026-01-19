using UnityEngine;

namespace WizardGame.Interfaces
{
    public interface IKnockbackable
    {
        void Knockback(float amount, Rigidbody2D source);
    }
}
