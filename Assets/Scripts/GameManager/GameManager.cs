using System;
using System.Collections.Generic;
using NUnit.Framework;
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
        [SerializeField] private GameObject levelUpPanel;

        //-- Instance --
        public static GameManager Instance { get; private set; }

        //-- Sub-Managers --
        private XPManager xpManager;
        private SpawnManager spawnManager;
        private InputManager inputManager;
        private InventoryManager inventoryManager;
        private UIManager uiManager;
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
        [HideInInspector]
        public float CurrentStageTime;

        //-- GameState --
        [HideInInspector]
        public GameState currentGameState;

        //-- Temp --
        public GameObject defaultSpellPrefab;


        public void Awake()
        {
            CreateInstance();
            InitManagers();
            SubscribeToEvents();

            CurrentWave = 0;
            currentGameState = GameState.Playing;
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

            uiManager = new UIManager(this, levelUpPanel);
            pocoManagers.Add(uiManager);
        }

        private void SubscribeToEvents()
        {
            EventManager.OnPlayerLevelUp += _ => PauseGame();
            EventManager.OnGameResumed += ResumeGame;
        }

        private void UnsubscribeFromEvents()
        {
            EventManager.OnPlayerLevelUp -= _ => PauseGame();
            EventManager.OnGameResumed -= ResumeGame;
        }

        private void TearDown()
        {
            UnsubscribeFromEvents();
        }

        public UIManager GetUIManager() => uiManager;

        private void PauseGame()
        {
            currentGameState = GameState.Paused;
            Time.timeScale = 0f;
        }

        private void ResumeGame()
        {
            currentGameState = GameState.Playing;
            Time.timeScale = 1f;
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
            TearDown();
        }
    }
}
