using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Stages
{
    public class EnemyStageDataSO
    {
        [SerializeField] private List<EnemyWaveDataSO> waves = new();

        public IReadOnlyList<EnemyWaveDataSO> Waves => waves;
    }
}
