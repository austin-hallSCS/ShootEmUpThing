using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WizardGame.Spells;

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

            titleText.text = data.SpellName;

            // FIXME: Null reference exception (needs to be fixed in inspector)
            iconImage.sprite = data.SpellIcon;
        }
    }
}
