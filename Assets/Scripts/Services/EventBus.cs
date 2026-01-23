using System;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Spells;

namespace WizardGame.Services
{
    /// <summary>
    /// Holds all Event Methods that interact between managers and Monobehavior scripts.
    /// </summary>
    public static class EventBus
    {
        #region Events
        //-- Game State --
        public static event Action OnGamePause;
        public static event Action OnGameResumed;
        // public static event Action OnGameOver;

        //-- Spawning --
        public static event Action<GameObject, Vector3, Quaternion> OnObjectSpawn;
        public static event Action<GameObject, GameObject> OnObjectDespawn;

        //-- Player/XP --
        public static event Action<int> OnPlayerLevelUp;
        public static event Action<GameObject> OnLevelUpSelection;
        public static event Action<SpellDataSO> OnSpellMaxLevel;

        // -- Collectible --
        public static event Action<int> OnExperienceCollected;
        // public static event Action<float> OnHealthCollected;

        // -- Combat/Enemy --
        public static event Action<EnemyController> OnEnemyDied;
        public static event Action<GameObject> OnEnemyDespawn;
        public static event Action OnNextWaveBegin;
        #endregion

        #region Public Publishers

        //-- Game State --
        public static void PublishGamePaused() => OnGamePause?.Invoke();
        public static void PublishGameResumed() => OnGameResumed?.Invoke();

        //-- Player/XP
        public static void PublishLevelUp(int newLevel) => OnPlayerLevelUp?.Invoke(newLevel);
        public static void PublishLevelUpSelection(GameObject prefab) => OnLevelUpSelection?.Invoke(prefab);
        public static void PublishSpellMaxLevel(SpellDataSO spellData) => OnSpellMaxLevel?.Invoke(spellData);

        //-- Spawning --
        public static void PublishObjectSpawn(GameObject prefab, Vector3 position, Quaternion rotation) => OnObjectSpawn?.Invoke(prefab, position, rotation);
        public static void PublishObjectDespawn(GameObject instance, GameObject originalPrefab) => OnObjectDespawn?.Invoke(instance, originalPrefab);

        //-- Collectible --
        public static void PublishExperienceCollected(int amount) => OnExperienceCollected?.Invoke(amount);

        //-- Combat/Enemy --
        public static void PublishEnemyDied(EnemyController enemy) => OnEnemyDied?.Invoke(enemy);
        public static void PublishEnemyDespawn(GameObject enemyPrefab) => OnEnemyDespawn?.Invoke(enemyPrefab);

        public static void PublishOnNextWaveBegin() => OnNextWaveBegin?.Invoke();

        #endregion
    }
}
