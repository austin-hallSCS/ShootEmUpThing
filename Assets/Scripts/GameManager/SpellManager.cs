using System.Collections.Generic;
using UnityEngine;
using WizardGame.Managers;
using WizardGame.Spells;
using WizardGame.Utils;

namespace WizardGame
{
    public class SpellManager : ManagerBase
    {
        [SerializeField] public IReadOnlyList<SpellController> AllSpells { get; private set; }

        public SpellManager(GameManager manager) : base(manager)
        {

        }

        protected override void SubscribeToEvents() { }
        protected override void UnsubscribeFromEvents() { }

        public List<GameObject> GetUpgradeOptions()
        {
            // TODO: Add weight to spells based on rarity

            // Make new shuffle bag with all spells
            ShuffleBag<GameObject> upgradeBag = new ShuffleBag<GameObject>(gameManager.AllSpellsDatabase.AllSpellPrefabs);
            List<GameObject> choices = new List<GameObject>();

            // Get 3 spells from the shuffle bag
            for (var i = 0; i < 3; i++)
            {
                choices.Add(upgradeBag.GetNext());
            }

            return choices;
        }

    }
}
