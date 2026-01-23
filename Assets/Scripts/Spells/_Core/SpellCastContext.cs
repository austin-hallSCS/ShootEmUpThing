using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class SpellCastContext
    {
        public Transform Caster;
        public SpellStats Stats;
        public SpellDataSO Data;
        public Vector3 TargetPosition;
        public Transform TargetEnemy;

        public SpellCastContext(Transform caster, SpellStats stats, SpellDataSO data)
        {
            Caster = caster;
            Stats = stats;
            Data = data;
        }
    }
}
