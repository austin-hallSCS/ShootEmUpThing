using System;
using UnityEngine;
using WizardGame.Player;
using WizardGame.Stages;

namespace WizardGame.Managers
{
    public enum GameState { Playing, Paused, GameOver }
    public class GameManager : MonoBehaviour
    {
        // Inspector-Editable Properties
        [field: SerializeField] public StageDataSO currentStageData { get; private set; }

        // Instance
        public static GameManager Instance  {get; private set; }

        // Sub-Managers
        public XPManager XPManager { get; private set; }
        public SpawnManager SpawnManager { get; private set; }

        public event Action NextWaveBegin;


        
        public void Awake()
        {
            CreateInstance();
            InitManagers();
            
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
            XPManager = new XPManager();
            SpawnManager = new SpawnManager(this, currentStageData);
        }

        private void OnDestroy()
        {
            if (SpawnManager != null)
            {
                SpawnManager.TearDown();
                SpawnManager = null;
            }
            // TODO: Add TearDowns for other Managers
        }

        public void LoadNewStage()
        {
            // TODO: Add logic for loading new stage
        }

    }
}
