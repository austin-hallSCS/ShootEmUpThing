using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class SwordBurstSpellController : ObjectSpellController
    {
        private List<GameObject> swordPool;

        protected void Awake()
        {
            swordPool = new List<GameObject>();
        }

        protected override void CastSpell()
        {
            base.CastSpell();
            ResetDuration();
            SpawnSwordFormation();
        }

        protected override void SpellActiveBehavior()
        {
            currentDurationTimeAt -= Time.deltaTime;

            if (currentDurationTimeAt <= 0)
            {
                SpellDeactivate();
            }
        }

        protected override void SpellDeactivate()
        {
            base.SpellDeactivate();

            foreach (var swordObject in swordPool)
            {
                if (swordObject != null) swordObject.SetActive(false);
            }
        }

        private void SpawnSwordFormation()
        {
            int totalSwords = (int)spellStats.GetStat(StatType.Amount).CurrentValue;

            float angleStep = 360f / totalSwords;

            for (int i = 0; i < totalSwords; i++)
            {
                float targetAngle = i * angleStep;
                ActivateSword(i, targetAngle);
            }
        }

        private void ActivateSword(int index, float startingAngle)
        {
            GameObject currentSword;

            if (index < swordPool.Count)
            {
                currentSword = swordPool[index];
                currentSword.SetActive(true);
            }
            else
            {
                currentSword = Instantiate(spellPrefab, transform.position, Quaternion.identity);
                swordPool.Add(currentSword);

                currentSword.transform.SetParent(transform);
            }

            if (currentSword.TryGetComponent(out SwordBurstGO gameObject))
            {
                // gameObject.Initialize(this, transform, startingAngle);
            }
        }
    }
}
