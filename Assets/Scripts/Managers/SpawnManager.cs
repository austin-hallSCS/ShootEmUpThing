using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stages;
using WizardGame.Utils;
using WizardGame.Services;

namespace WizardGame.Managers
{
    public enum ScreenSide { Top, Right, Bottom, Left }
    public class SpawnManager : ManagerBase
    {
        private StageDataSO stageData;

        private Coroutine spawnRoutine = null;

        private int currentEnemyCount;
        private List<GameObject> despawnedEnemies;

        public SpawnManager(GameManager manager, StageDataSO data) : base(manager)
        {
            stageData = data;

            despawnedEnemies = new List<GameObject>();

            SubscribeToEvents();
        }

        protected override void SubscribeToEvents()
        {
            EventBus.OnNextWaveBegin += HandleNextWaveBegin;
            EventBus.OnEnemyDied += HandleEnemyDied;
            EventBus.OnEnemyDespawn += HandleEnemyDespawn;
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
            if (currentWave >= stageData.Waves.Count) yield break;

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
            if (enemyPrefabs == null || enemyPrefabs.Count == 0)
            {
                Debug.LogWarning("Enemy prefabs list is empty. Cannot spawn.");
                return;
            }

            GameObject enemyToSpawn;

            Vector2 nextSpawnPosition = GameManager.Instance.MainCamera.ViewportToWorldPoint(GetRandomSpawnPoint());

            if (despawnedEnemies.Count > 0)
            {
                int lastIndex = despawnedEnemies.Count - 1;
                enemyToSpawn = despawnedEnemies[lastIndex];

                despawnedEnemies.RemoveAt(lastIndex);

                enemyToSpawn.transform.position = nextSpawnPosition;
                enemyToSpawn.SetActive(true);

                // TODO: Make dictionary pool to handle waves that have different types of enemies
            }
            else
            {
                enemyToSpawn = enemyPrefabs.GetRandomItem();
                Object.Instantiate(enemyToSpawn, nextSpawnPosition, Quaternion.identity);
            }

            currentEnemyCount++;
            // Debug.Log($"currentEnemyCount: {currentEnemyCount}");
        }

        private void SpawnBoss(GameObject bossPrefab)
        {
            if (bossPrefab == null)
            {
                Debug.LogWarning("Attempted to spawn boss in wave where Boss Prefab is null!");
                return;
            }
            // TODO: Add boss spawning logic here.
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
            // Debug.Log($"currentEnemyCount: {currentEnemyCount}");
        }

        private void HandleEnemyDespawn(GameObject enemy)
        {
            currentEnemyCount--;
            enemy.SetActive(false);
            despawnedEnemies.Add(enemy);
        }

        #endregion

        protected override void UnsubscribeFromEvents()
        {
            EventBus.OnNextWaveBegin -= HandleNextWaveBegin;
            EventBus.OnEnemyDied -= HandleEnemyDied;
            EventBus.OnEnemyDespawn -= HandleEnemyDespawn;
        }

        protected override void OnTearDown()
        {
            PauseSpawning();

            // Destroy all pool objects to clear memory fully
            foreach (var enemy in despawnedEnemies) if (enemy) Object.Destroy(enemy);
            despawnedEnemies.Clear();
        }
    }
}