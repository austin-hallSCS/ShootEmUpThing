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
        // Hidden properties - set by SpellGO when building payload
        [HideInInspector]
        public float DamageAmount;

        [HideInInspector]
        public float KnockbackAmount;

        [HideInInspector]
        public Vector2 SourcePosition;

        // Inspector properties - set in SpellData asset.
        public StatusEffectType StatusEffect;
        // May eventually make these set by SpellStats (Duration, etc.)
        public float StatusDuration;
        public float StatusStrength;
    }
}
