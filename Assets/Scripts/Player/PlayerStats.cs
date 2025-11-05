using UnityEngine;

namespace WizardGame.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat Level { get; private set; }
        [field: SerializeField] public Stat ExperienceAmount { get; private set; }
        [field: SerializeField] public Stat MovementSpeed { get; private set; }

        [Header("Bonuses")]
        [field: SerializeField] public Stat HealthBonus { get; private set; }
        [field: SerializeField] public Stat DamageBonus { get; private set; }
        [field: SerializeField] public Stat AreaBonus { get; private set; }
        [field: SerializeField] public Stat SpeedBonus { get; private set; }
        [field: SerializeField] public Stat CoolDownTimeBonus { get; private set; }
        [field: SerializeField] public Stat KnockbackBonus { get; private set; }
        [field: SerializeField] public Stat ProjectileBonus { get; private set; }
        [field: SerializeField] public Stat ProjectileIntervalTimeBonus { get; private set; }
        [field: SerializeField] public Stat DurationTimeBonus { get; private set; }
        [field: SerializeField] public Stat PierceBonus { get; private set; }

        public void Awake()
        {
            Health.Init();
            Level.Init();
            ExperienceAmount.Init();
            MovementSpeed.Init();

            HealthBonus.Init();
            DamageBonus.Init();
            AreaBonus.Init();
            SpeedBonus.Init();
            CoolDownTimeBonus.Init();
            KnockbackBonus.Init();
            ProjectileBonus.Init();
            ProjectileIntervalTimeBonus.Init();
            DurationTimeBonus.Init();
            PierceBonus.Init();
        }
    }
}
