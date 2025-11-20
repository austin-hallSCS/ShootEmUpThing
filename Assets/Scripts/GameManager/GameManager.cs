using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Player;
using WizardGame.Stages;

namespace WizardGame.Managers
{
    public enum GameState { Playing, Paused, GameOver }
    public class GameManager : MonoBehaviour
    {
        // Inspector-Editable Properties
        [field: SerializeField] public StageDataSO CurrentStageData { get; private set; }
        [field: SerializeField] public PlayerController PlayerController { get; private set;}

        // Instance
        public static GameManager Instance  {get; private set; }

        // Sub-Managers
        private XPManager xpManager;
        private SpawnManager spawnManager;
        private List<ManagerBase> pocoManagers = new();

        // Global Events
        public event Action<EnemyController> OnEnemyDied;

        // Waves
        public event Action NextWaveBegin;
        public int CurrentWave { get; private set; }

        // Time
        public float CurrentStageTime;

        
        public void Awake()
        {
            CreateInstance();
            InitManagers();

            CurrentWave = 0;
        }

        public void Update()
        {
            CurrentStageTime += Time.deltaTime;
            CheckForNextWave();
        }

        // Creates a new instance if there is not one already, makes sure there is not two instances
        private void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void InitManagers()
        {
            xpManager = new XPManager();
            pocoManagers.Add(xpManager);

            spawnManager = new SpawnManager(this, CurrentStageData);
            pocoManagers.Add(spawnManager);
        }

        private void CheckForNextWave()
        {
            if (CurrentWave + 1 < CurrentStageData.Waves.Count)
            {                
                if (CurrentStageTime >= CurrentStageData.Waves[CurrentWave + 1].StartTime)
                {
                    StartNextWave();
                }
            }
        }

        private void StartNextWave()
        {
            CurrentWave++;
            NextWaveBegin?.Invoke();
        }

        public void LoadNewStage()
        {
            // TODO: Add logic for loading new stage
        }

        private void OnDestroy()
        {
            foreach (ManagerBase manager in pocoManagers)
            {
                manager?.TearDown();
            }
        }
    }
}
