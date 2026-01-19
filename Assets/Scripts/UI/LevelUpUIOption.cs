using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WizardGame.Spells;
using WizardGame.Stats;

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

        public void Configure(Sprite icon, string description)
        {
            if (icon == null || description == null) return;

            titleText.text = description;

            iconImage.sprite = icon;
        }
    }
}
