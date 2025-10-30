using UnityEngine;

namespace WizardGame.SpellSystem
{
    [CreateAssetMenu(fileName = "SpellData", menuName ="ScriptableObjects/SpellControllerScriptableObject")]
    public class SpellDataSO : ScriptableObject
    {   
        [Header("Unity References")]
        [SerializeField] public GameObject SpellPrefab;

        [Header("Data")]
        [SerializeField] public float SpellRarity;

        [Header("Damage")]
        [SerializeField] public float spellDamageAmountBase;

        [Header("Area")]
        [SerializeField] private float areaMultMax;
        [SerializeField] public float AreaMultBase;

        [Header("Speed")]
        [SerializeField] public float SpeedAmountMax;
        [SerializeField] public float SpeedAmountBase;

        [Header("Cooldown")]
        [SerializeField] public float CooldownTimeMax;
        [SerializeField] public float CooldownTimeBase;

        [Header("Knockback")]
        [SerializeField] public float KnockBackAmountMax;
        [SerializeField] public float KnockBackAmountBase;

        [Header("Projectile")]
        [SerializeField] public float ProjectileAmountMax;
        [SerializeField] public float ProjectileAmountBase;

        [Header("Duration")]
        [SerializeField] public float DurationTimeMax;
        [SerializeField] public float DurationTimebase;

        [Header("Pierce")]
        [SerializeField] public float PierceAmountMax;
        [SerializeField] public float PierceAmountBase;
    }
}
