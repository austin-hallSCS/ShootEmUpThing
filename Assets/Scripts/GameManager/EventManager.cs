using System;
using UnityEditor;
using WizardGame.Enemy;

namespace WizardGame.Managers
{
    public static class EventManager
    {
        #region Events
        //-- Game State --
        public static event Action OnGamePause;
        // public static event Action OnGameResumed;
        // public static event Action OnGameOver;

        //-- Player/XP --
        public static event Action<int> OnPlayerLevelUp;

        // -- Collectible --
        public static event Action<int> OnExperienceCollected;
        // public static event Action<float> OnHealthCollected;

        // -- Combat/Enemy --
        public static event Action<EnemyController> OnEnemyDied;
        public static event Action OnNextWaveBegin;
        #endregion

        #region Public Publishers

        //-- Game State --
        public static void PublishGamePaused() => OnGamePause?.Invoke();

        //-- Player/XP
        public static void PublishLevelUp(int newLevel) => OnPlayerLevelUp?.Invoke(newLevel);

        //-- Collectible --
        public static void PublishExperienceCollected(int amount) => OnExperienceCollected?.Invoke(amount);

        //-- Combat/Enemy --
        public static void PublishEnemyDied(EnemyController enemy) => OnEnemyDied?.Invoke(enemy);

        public static void PublishOnNextWaveBegin() => OnNextWaveBegin?.Invoke();

        #endregion
    }
}
