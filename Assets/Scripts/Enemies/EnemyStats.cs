using UnityEngine;

namespace WizardGame.Stats
{
    public class EnemyStats : MonoBehaviour
    {
        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat DamageAmount { get; private set; }
        [field: SerializeField] public Stat SpeedAmount { get; private set; }
        [field: SerializeField] public Stat ExperienceAmount { get; private set; }

        public void Awake()
        {
            Health.Init();
            DamageAmount.Init();
            SpeedAmount.Init();
            ExperienceAmount.Init();
        }
    }
}
