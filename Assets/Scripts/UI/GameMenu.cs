using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardGame.UI
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private string gameSceneName;

        public void Play()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
