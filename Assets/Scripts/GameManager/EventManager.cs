using System;
using WizardGame.Enemy;

namespace WizardGame.Managers
{
    public static class EventManager
    {
        //-- Core Game Stat Events --
        // public static event Action OnGamePause;
        // public static event Action OnGameResumed;
        // public static event Action OnGameOver;

        //-- Player/XP Events --
        public static event Action<int> OnPlayerLevelUp;

        // -- Collectible Events --
        public static event Action<int> OnExperienceCollected;
        // public static event Action<float> OnHealthCollected;

        // -- Combat/Enemy Events --
        public static event Action<EnemyController> OnEnemyDied;
        public static event Action OnNextWaveBegin;

#region Public Publishers

        public static void PublishLevelUp(int newLevel) => OnPlayerLevelUp?.Invoke(newLevel);
        public static void PublishEnemyDied(EnemyController enemy) => OnEnemyDied?.Invoke(enemy);
        public static void PublishExperienceCollected(int amount) => OnExperienceCollected?.Invoke(amount);
        public static void PublishOnNextWaveBegin() => OnNextWaveBegin?.Invoke();

#endregion
    }
}
