using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class SpellCastContext
    {
        public Transform Caster;
        public SpellController Controller;
        public SpellStats Stats;
        public SpellDataSO Data;
        public Vector3 TargetPosition;
        public Transform TargetEnemy;

        public List<GameObject> SpawnedInstances = new List<GameObject>();

        public SpellCastContext(Transform caster, SpellController controller, SpellStats stats, SpellDataSO data)
        {
            Caster = caster;
            Controller = controller;
            Stats = stats;
            Data = data;

            //FIXME: Get Enemy layermask from static helper once it is created
        }
    }
}
