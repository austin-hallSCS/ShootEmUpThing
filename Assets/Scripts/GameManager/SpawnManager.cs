using System.Collections;
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

            GameManager.Instance.NextWaveBegin += OnNextWaveBegin;
        }

        public void StartSpawning()
        {
            // Avoids duplicate spawnRoutines
            if (spawnRoutine != null) return;

            spawnRoutine = GameManager.Instance.StartCoroutine(SpawnLoop());
        }

        public void PauseSpawning()
        {
            if (spawnRoutine != null)
            {
                coroutineRunner.StopCoroutine(SpawnLoop());
                spawnRoutine = null;
            }
        }

        private void OnNextWaveBegin()
        {
            PauseSpawning();

            currentWave++;

            StartSpawning();
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                SpawnEnemy();

                yield return new WaitForSeconds(stageData.Waves[currentWave].SpawnInterval);
            }
        }

        private void SpawnEnemy()
        {
            // TODO: Add enemy spawning logic
        }

        public void TearDown()
        {
            GameManager.Instance.NextWaveBegin -= OnNextWaveBegin;

            if (spawnRoutine != null) coroutineRunner.StopCoroutine(SpawnLoop());

            Debug.Log("SpawnManager TearDown");
        }
    }
}