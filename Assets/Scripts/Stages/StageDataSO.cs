using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Stages
{

    [CreateAssetMenu(fileName = "StageDataSO", menuName = "Stages/Stage Data")]
    public class StageDataSO : ScriptableObject
    {
        [field: Header("Identity")]
        [field: SerializeField] public string Name { get; private set; }
        
        [field: Header("Waves")]
        [field: Tooltip("The list of all waves in this stage, in order.")]
        [field: SerializeField] private List<EnemyWave> waves = new List<EnemyWave>();
        public IReadOnlyList<EnemyWave> Waves => waves;

    }
}