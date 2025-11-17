using UnityEngine;
using WizardGame.Player;
using WizardGame.Stages;

namespace WizardGame.Managers
{
    public enum GameState { Playing, Paused, GameOver }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance  {get; private set; }

        [SerializeField] public GameStageDataSO currentStageData { get; private set; }

        public XPManager XPManager { get; private set; }
        public SpawnManager SpawnManager { get; private set; }


        // Creates a new instance if there is not one already, makes sure there is not two instances
        public void Awake()
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

            XPManager = new XPManager();
            SpawnManager = new SpawnManager(this, currentStageData);
        }
        
    }
}
