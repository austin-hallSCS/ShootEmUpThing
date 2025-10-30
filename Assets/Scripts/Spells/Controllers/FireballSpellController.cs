using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utils;

namespace WizardGame.SpellSystem
{
    public class FireballSpellController : ProjectileSpellController
    {
        [SerializeField] private List<GameObject> spawnPoints;

        private ShuffleBag<Vector2> directionPicker = new ShuffleBag<Vector2>(Utils.WorldSenses.cardinalDirections);

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void SpellActiveBehavior()
        {
            base.SpellActiveBehavior();

            // Vector2 direction = WorldSenses.GetRandomDirection();
            // Quaternion rotation = WorldSenses.VectorDirectionToRotation(direction);

            // spellGameObject = Instantiate(spellData.SpellPrefab, (Vector2)transform.position + Vector2.up * 0.5f, rotation, transform);
            // spellGameObject.GetComponent<SpellGO>().Launch(direction, force);
        }
        
        private void HandleDirection()
        {
            Vector2 nextDirection = directionPicker.GetNext();
        }
    }

}