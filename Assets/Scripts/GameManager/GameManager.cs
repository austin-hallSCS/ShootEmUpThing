using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Player;
using WizardGame.Stages;

namespace WizardGame.Managers
{
    public enum GameState { Playing, Paused, GameOver }
    public enum Direction { Up, Down, Left, Right }
    public class GameManager : MonoBehaviour
    {
        //-- Inspector-Editable Properties --
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public StageDataSO CurrentStageData { get; private set; }
        [field: SerializeField] public PlayerController PlayerController { get; set; }

        //-- Instance --
        public static GameManager Instance { get; private set; }

        //-- Sub-Managers --
        private XPManager xpManager;
        private SpawnManager spawnManager;
        private InputManager inputManager;
        private InventoryManager inventoryManager;
        private List<ManagerBase> pocoManagers = new();

        //-- Input --
        public Vector2 MoveInput
        {
            get
            {
                if (inputManager == null) return Vector2.zero;
                return inputManager.MoveInput;
            }
        }

        //-- Waves --
        public event Action NextWaveBegin;
        public int CurrentWave { get; private set; }

        //-- Time --
        public float CurrentStageTime;

        //-- Temp --
        public GameObject defaultSpellPrefab;


        public void Awake()
        {
            CreateInstance();
            InitManagers();

            CurrentWave = 0;
        }

        public void Start()
        {
            if (PlayerController != null)
            {
                AddStartingSpell(defaultSpellPrefab);
            }
            else
            {
                Debug.LogWarning("PlayerController is null on GameManager!");
            }

            spawnManager.StartSpawning();
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
            xpManager = new XPManager(this);
            pocoManagers.Add(xpManager);

            spawnManager = new SpawnManager(this, CurrentStageData);
            pocoManagers.Add(spawnManager);

            inputManager = new InputManager(this);
            pocoManagers.Add(inputManager);

            inventoryManager = new InventoryManager(this);
            pocoManagers.Add(inventoryManager);
        }

        // Temp for testing.
        public void AddStartingSpell(GameObject spellPrefab)
        {
            inventoryManager.AddSpell(spellPrefab);
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
