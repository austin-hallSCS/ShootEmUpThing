using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stages;
using WizardGame.Utils;

namespace WizardGame.Managers
{
    public enum ScreenSide { Top, Right, Bottom, Left }
    public class SpawnManager : ManagerBase
    {
        private GameManager gameManager;
        private StageDataSO stageData;

        private Coroutine spawnRoutine = null;

        private int currentEnemyCount;

        public SpawnManager(GameManager manager, StageDataSO data)
        {
            gameManager = manager;
            stageData = data;

            SubscribeToEvents();
        }

        protected override void SubscribeToEvents()
        {
            EventManager.OnNextWaveBegin += HandleNextWaveBegin;
            EventManager.OnEnemyDied += HandleEnemyDied;
        }

        public void StartSpawning()
        {
            // Avoids duplicate spawnRoutines
            if (spawnRoutine != null) return;

            spawnRoutine = gameManager.StartCoroutine(SpawnLoop());
        }

        public void PauseSpawning()
        {
            if (spawnRoutine != null)
            {
                gameManager.StopCoroutine(spawnRoutine);
                spawnRoutine = null;
            }
        }

        private IEnumerator SpawnLoop()
        {
            var currentWave = gameManager.CurrentWave;
            var waveData = stageData.Waves[currentWave];

            while (true)
            {
                if (currentEnemyCount < waveData.EnemyMaximum)
                {
                    SpawnEnemy(waveData.EnemyPrefabs);
                }

                yield return new WaitForSeconds(waveData.SpawnInterval);
            }
        }

        private void SpawnEnemy(List<GameObject> enemyPrefabs)
        {
            GameObject enemyPrefabToSpawn = GetRandom.FromList<GameObject>(enemyPrefabs);

            Vector2 nextSpawnPosition = GameManager.Instance.MainCamera.ViewportToWorldPoint(GetRandomSpawnPoint());

            Object.Instantiate(enemyPrefabToSpawn, nextSpawnPosition, Quaternion.identity);

            currentEnemyCount++;
            Debug.Log($"currentEnemyCount: {currentEnemyCount}");
        }

        private void SpawnBoss(GameObject bossPrefab)
        {
            if (bossPrefab == null)
            {
                Debug.LogWarning("Attempted to spawn boss in wave where Boss Prefab is null!");
            }
        }

        private Vector2 GetRandomSpawnPoint()
        {
            float roll = Random.value;
            Direction nextSide = GetDirectionWeighted();

            switch (nextSide)
            {
                case Direction.Up:
                    return new Vector2(roll, 1.1f);
                case Direction.Right:
                    return new Vector2(1.1f, roll);
                case Direction.Down:
                    return new Vector2(roll, -0.1f);
                case Direction.Left:
                    return new Vector2(-0.1f, roll);
                default:
                    Debug.LogWarning("GetRandomSpawnPoint did not choose a valid side.");
                    return new Vector2(roll, 1.1f);
            }
        }

        // Uses cumulative probability to determine a random direction, weighted by the direction the player is moving
        // Don't worry about the math too much. It makes my head hurt but it works.s
        private Direction GetDirectionWeighted()
        {
            float wUp = 1f;
            float wRight = 1f;
            float wDown = 1f;
            float wLeft = 1f;
            float bonusWeight = 2f;

            float inputX = GameManager.Instance.MoveInput.x;
            float inputY = GameManager.Instance.MoveInput.y;

            // Add weight based on player input
            if (inputX > 0) wRight += bonusWeight;
            else if (inputX < 0) wLeft += bonusWeight;

            if (inputY > 0) wUp += bonusWeight;
            else if (inputY < 0) wDown += bonusWeight;

            float totalWeight = wUp + wRight + wDown + wLeft;

            float roll = Random.Range(0f, totalWeight);

            // Apply subtraction method to determine direction
            if (roll < wUp) return Direction.Up;
            roll -= wUp;

            if (roll < wRight) return Direction.Right;
            roll -= wRight;

            if (roll < wDown) return Direction.Down;
            roll -= wDown;

            return Direction.Left;
        }

        #region Event Handlers

        private void HandleNextWaveBegin()
        {
            PauseSpawning();

            var currentWaveData = stageData.Waves[gameManager.CurrentWave];
            if (currentWaveData.BossPrefab != null)
            {
                SpawnBoss(currentWaveData.BossPrefab);
            }

            StartSpawning();
        }

        private void HandleEnemyDied(EnemyController enemy)
        {
            currentEnemyCount--;
            Debug.Log($"currentEnemyCount: {currentEnemyCount}");
        }

        #endregion

        protected override void UnsubscribeFromEvents()
        {
            EventManager.OnNextWaveBegin -= HandleNextWaveBegin;
            EventManager.OnEnemyDied -= HandleEnemyDied;
        }

        public override void TearDown()
        {
            UnsubscribeFromEvents();

            PauseSpawning();
        }
    }
}