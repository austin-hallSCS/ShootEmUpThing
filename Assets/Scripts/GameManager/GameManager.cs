using UnityEngine;
using WizardGame.Player;

namespace WizardGame.Managers
{
    public enum GameState { Playing, Paused, GameOver }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance  {get; private set; }

        public XPManager XPManager { get; private set; }

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
        }
        
    }
}
