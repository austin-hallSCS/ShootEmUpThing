using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Player;
using WizardGame.Spells;
using WizardGame.Stages;

namespace WizardGame.Managers
{
    public enum GameState { Playing, Paused, GameOver }
    public enum Direction { Up, Down, Left, Right }
    public class GameManager : MonoBehaviour
    {
        //-- Inspector-Editable Properties --

        [field: Header("--- Scene References ---")]
        [field: Tooltip("The main camera used for world to screen calculations.")]
        [field: SerializeField] public Camera MainCamera { get; private set; }

        [field: Tooltip("Reference to the player script.")]
        [field: SerializeField] public PlayerController PlayerController { get; set; }

        [field: Tooltip("The UI panel that appears when a player levels up.")]
        [SerializeField] private GameObject levelUpPanel;

        [field: Space(10)]
        [field: Header("--- Game Data ---")]

        [field: Tooltip("The database containing all possible spells.")]
        [field: SerializeField] public SpellDatabaseSO AllSpellsDatabase { get; private set; }

        [field: Tooltip("Configuration for the current level (waves, enemies, etc.)")]
        [field: SerializeField] public StageDataSO CurrentStageData { get; private set; }

        //-- Instance --
        public static GameManager Instance { get; private set; }

        //-- Sub-Managers --
        private XPManager xpManager;
        private SpawnManager spawnManager;
        private InputManager inputManager;
        private InventoryManager inventoryManager;
        private UIManager uiManager;
        private SpellManager spellManager;
        private Dictionary<Type, ManagerBase> pocoManagers = new Dictionary<Type, ManagerBase>();

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
        public GameState CurrentGameState;

        //-- Temp --
        public GameObject DefaultSpellPrefab;


        public void Awake()
        {
            CreateInstance();
            InitManagers();
            SubscribeToEvents();

            CurrentWave = 0;
            CurrentGameState = GameState.Playing;
        }

        public void Start()
        {
            if (PlayerController != null)
            {
                AddStartingSpell(DefaultSpellPrefab);
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
            pocoManagers.Add(typeof(XPManager), xpManager);

            spawnManager = new SpawnManager(this, CurrentStageData);
            pocoManagers.Add(typeof(SpawnManager), spawnManager);

            inputManager = new InputManager(this);
            pocoManagers.Add(typeof(InputManager), inputManager);

            inventoryManager = new InventoryManager(this);
            pocoManagers.Add(typeof(InventoryManager), inventoryManager);

            uiManager = new UIManager(this, levelUpPanel);
            pocoManagers.Add(typeof(UIManager), uiManager);

            spellManager = new SpellManager(this);
            pocoManagers.Add(typeof(SpellManager), spellManager);
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

        // Called by poco managers to get a reference to another poco manager.
        // This is so each manager does not have to create a reference to all others, they just go through the GameManager.
        public T GetManager<T>() where T : ManagerBase
        {
            if (pocoManagers.TryGetValue(typeof(T), out ManagerBase manager))
            {
                return (T)manager;
            }
            return null;
        }

        // public UIManager GetUIManager() => uiManager;

        // public InventoryManager GetInventoryManager() => inventoryManager;

        private void PauseGame()
        {
            CurrentGameState = GameState.Paused;
            Time.timeScale = 0f;
        }

        private void ResumeGame()
        {
            CurrentGameState = GameState.Playing;
            Time.timeScale = 1f;
        }

        // Temp for testing.
        public void AddStartingSpell(GameObject spellPrefab)
        {
            inventoryManager.ProcessLevelUp(spellPrefab);
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
            foreach (ManagerBase manager in pocoManagers.Values)
            {
                manager?.TearDown();
            }
            TearDown();
        }
    }
}
