using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

namespace WizardGame.Stages
{
    [System.Serializable]
    public class EnemyWave
    {
        [field: Tooltip("What time (in seconds) does this wave start?")]
        [field: SerializeField] public float StartTime { get; private set; }

        [field: Header("Spawning Rules")]
        [field: Tooltip("Time (in seconds) between spawns.")]
        [field: SerializeField] public float SpawnInterval { get; private set; }

        [field: Header("Enemy Prefabs")]
        [field: SerializeField] public GameObject BossPrefab { get; private set; }
        [field: SerializeField] public GameObject[] EnemyPrefabs { get; private set; }

        [field: Header("Limits")]
        [field: SerializeField] public int EnemyMinimum { get; private set; }
        [field: SerializeField] public int EnemyMaximum { get; private set; }

    }
}
