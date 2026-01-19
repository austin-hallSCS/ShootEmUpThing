using System.Collections.Generic;
using UnityEngine;
using WizardGame.Managers;
using WizardGame.Spells;

namespace WizardGame.UI
{
    /// <summary>
    /// Controls the level up screen, and the option cards that are displayed there.
    /// </summary>
    public class LevelUpUIProxy : MonoBehaviour
    {
        [SerializeField] private LevelUpUIOption[] optionCards;

        // Called by UIManager on level up
        public void UpdateUpgradeOptions(List<GameObject> spellPrefabs)
        {
            for (int i = 0; i < optionCards.Length; i++)
            {
                if (i < spellPrefabs.Count)
                {
                    // Get the SpellController from the prefab
                    SpellController spell = spellPrefabs[i].GetComponent<SpellController>();

                    // Get the data
                    SpellDataSO data = spell.SpellData;

                    // Get the modifier descriptions
                    // TODO: Figure out how to move this to a poco manager, to keep this class cleaner
                    string description = GameManager.Instance.GetManager<SpellManager>().GetLevelUpDescriptions(data);

                    // Update the UI card
                    optionCards[i].Configure(data.SpellIcon, description);

                    // Ensure the button is visible
                    optionCards[i].gameObject.SetActive(true);
                }
                else
                {
                    // Hide extra buttons if there are no more spells to select
                    optionCards[i].gameObject.SetActive(false);
                }
            }
        }

        public void SelectOption1() => GameManager.Instance.GetManager<UIManager>().SelectUpgrade(0);
        public void SelectOption2() => GameManager.Instance.GetManager<UIManager>().SelectUpgrade(1);
        public void SelectOption3() => GameManager.Instance.GetManager<UIManager>().SelectUpgrade(2);
    }
}
