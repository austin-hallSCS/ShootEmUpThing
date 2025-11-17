using UnityEngine;

namespace WizardGame.Stages
{
    public class GameStageDataSO : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] public string Name { get; private set; }
        
        [SerializeField] public EnemyStageDataSO EnemyStageData { get; private set; }

    }
}