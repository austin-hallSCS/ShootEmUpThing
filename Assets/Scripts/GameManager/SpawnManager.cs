using UnityEngine;
using WizardGame.Stages;

namespace WizardGame.Managers
{
    public class SpawnManager
    {
        public GameManager gameManager { get; private set; }
        public GameStageDataSO gameStageData { get; private set; }

        public SpawnManager(GameManager gameManager, GameStageDataSO gameLevelData)
        {
            this.gameManager = gameManager;
            this.gameStageData = gameLevelData;
        }
    }
}