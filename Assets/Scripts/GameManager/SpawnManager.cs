using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stages;

namespace WizardGame.Managers
{
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

        private void SpawnEnemy(GameObject[] enemyPrefabs)
        {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            {
                Debug.LogWarning("Enemy prefabs list is empty for the current wave. Cannot spawn.");
                return;
            }
            int listCount = enemyPrefabs.Length;
            int choice = Random.Range(0, listCount);
            GameObject enemyPrefabToSpawn = enemyPrefabs[choice];
            // TODO: Figure out spawn area for enemies
            Vector3 spawnPositionPlaceHolder = new Vector3(10, 0, 0);

            Object.Instantiate(enemyPrefabToSpawn, spawnPositionPlaceHolder, Quaternion.identity);
            currentEnemyCount++;
        }

        private void SpawnBoss(GameObject bossPrefab)
        {
            if (bossPrefab == null)
            {
                Debug.LogWarning("Attempted to spawn boss in wave where Boss Prefab is null!");
            }
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

            Debug.Log("SpawnManager TearDown");
        }
    }
}