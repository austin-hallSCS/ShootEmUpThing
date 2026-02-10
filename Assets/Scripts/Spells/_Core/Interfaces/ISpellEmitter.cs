using System.Collections;

namespace WizardGame.Spells
{
    public interface ISpellEmitter
    {
        IEnumerator Execute(SpellCastContext context);

        void Despawn(SpellCastContext context);
    }
}
