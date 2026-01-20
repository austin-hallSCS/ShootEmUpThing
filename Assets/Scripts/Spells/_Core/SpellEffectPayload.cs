using UnityEngine;

namespace WizardGame.Spells
{
    public enum StatusEffectType
    {
        None,
        Burn,
        Freeze,
        Confuse,
        Stun,
        Taunt
    }

    [System.Serializable]
    public class SpellEffectPayload
    {
        public float DamageAmount;
        public StatusEffectType StatusEffect;
        public float StatusDuration;
        public float StatusStrength;
    }
}
