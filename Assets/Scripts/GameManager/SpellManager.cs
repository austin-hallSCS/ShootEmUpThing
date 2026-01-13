using System.Collections.Generic;
using UnityEngine;
using WizardGame.Spells;

namespace WizardGame
{
    public class SpellManager
    {
        [SerializeField] public IReadOnlyList<SpellController> AllSpells { get; private set; }

    }
}
