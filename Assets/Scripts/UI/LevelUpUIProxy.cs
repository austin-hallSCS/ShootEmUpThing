using UnityEngine;
using WizardGame.Managers;

namespace WizardGame.UI
{
    public class LevelUpUIProxy : MonoBehaviour
    {
        public void SelectOption1()
        {
            GameManager.Instance.GetUIManager().SelectUpgrade(0);
        }

        public void SelectOption2()
        {
            GameManager.Instance.GetUIManager().SelectUpgrade(1);
        }

        public void SelectOption3()
        {
            GameManager.Instance.GetUIManager().SelectUpgrade(2);
        }
    }
}
