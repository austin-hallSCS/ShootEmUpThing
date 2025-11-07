using UnityEngine;

namespace WizardGame.Stats
{
    public class SpellStats : MonoBehaviour
    {
        [field: SerializeField] public Stat Rarity { get; private set; }
        [field: SerializeField] public Stat Level { get; private set; }
        [field: SerializeField] public Stat DamageAmount { get; private set; }
        [field: SerializeField] public Stat AreaAmount { get; private set; }
        [field: SerializeField] public Stat SpeedAmount { get; private set; }
        [field: SerializeField] public Stat CoolDownTime { get; private set; }
        [field: SerializeField] public Stat KnockbackAmount { get; private set; }
        [field: SerializeField] public Stat ProjectileAmount { get; private set; }
        [field: SerializeField] public Stat ProjectileIntervalTime { get; private set; }
        [field: SerializeField] public Stat DurationTime { get; private set; }
        [field: SerializeField] public Stat PierceAmount { get; private set; }

        public void Awake()
        {
            Rarity.Init();
            Level.Init();
            Level.SetMaxValue(10);
            DamageAmount.Init();
            AreaAmount.Init();
            SpeedAmount.Init();
            CoolDownTime.Init();
            KnockbackAmount.Init();
            ProjectileAmount.Init();
            ProjectileIntervalTime.Init();
            DurationTime.Init();
            PierceAmount.Init();
        }
    }
}