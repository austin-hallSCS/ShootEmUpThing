using System.Collections;
using Mono.Cecil;
using UnityEngine;
using WizardGame.Stages;

namespace WizardGame.Managers
{
    public class SpawnManager
    {
        private GameManager coroutineRunner;
        private StageDataSO stageData;

        private Coroutine spawnRoutine = null;

        private int currentWave = 0;

        public SpawnManager(GameManager runner, StageDataSO stageData)
        {
            this.coroutineRunner = runner;
            this.stageData = stageData;

            runner.NextWaveBegin += OnNextWaveBegin;

            StartSpawning();
        }

        public void StartSpawning()
        {
            // Avoids duplicate spawnRoutines
            if (spawnRoutine != null) return;

            spawnRoutine = coroutineRunner.StartCoroutine(SpawnLoop());
        }

        public void PauseSpawning()
        {
            if (spawnRoutine != null)
            {
                coroutineRunner.StopCoroutine(spawnRoutine);
                spawnRoutine = null;
            }
        }

        private void OnNextWaveBegin()
        {
            PauseSpawning();

            var currentWaveData = stageData.Waves[GameManager.Instance.CurrentWave];
            if (currentWaveData.BossPrefab != null)
            {
                SpawnBoss(currentWaveData.BossPrefab);
            }

            StartSpawning();
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                var waveData = stageData.Waves[GameManager.Instance.CurrentWave];
                
                // TODO: Add EnemyMax/EnemyMin check before spawning

                SpawnEnemy(waveData.EnemyPrefabs);

                yield return new WaitForSeconds(stageData.Waves[currentWave].SpawnInterval);
            }
        }

        private void SpawnEnemy(GameObject[] enemyPrefabs)
        {
            // TODO: Add enemy spawning logic
        }

        private void SpawnBoss(GameObject bossPrefab)
        {
            // TODO: Add boss spawning logic
        }

        public void TearDown()
        {
            coroutineRunner.NextWaveBegin -= OnNextWaveBegin;

            PauseSpawning();

            Debug.Log("SpawnManager TearDown");
        }
    }
}