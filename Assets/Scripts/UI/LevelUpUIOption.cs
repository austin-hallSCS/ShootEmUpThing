using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WizardGame.Spells;
using WizardGame.Stats;
using Mono.Cecil;

namespace WizardGame.UI
{
    /// <summary>
    /// Buttons that display on level up screen. Controlled by LevelUpUIProxy
    /// </summary>
    public class LevelUpUIOption : MonoBehaviour
    {
        [Header("Child Components")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image iconImage;

        public void Configure(SpellDataSO data)
        {
            if (data == null) return;
            SpellLevelData levelData = data.GetLevelData(2);

            titleText.text = levelData.GetAllDescriptions();

            iconImage.sprite = data.SpellIcon;
        }
    }
}
